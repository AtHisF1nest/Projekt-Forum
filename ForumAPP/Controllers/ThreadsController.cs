using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumAPP.Data;
using ForumAPP.Data.Models;
using ForumAPP.Dtos;
using ForumAPP.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumAPP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ThreadsController : ControllerBase
    {
        private readonly ForumContext _forumContext;
        private readonly IForumHelper _forumHelper;

        public ThreadsController(ForumContext forumContext, IForumHelper forumHelper)
        {
            _forumContext = forumContext;
            _forumHelper = forumHelper;
        }
        
        [HttpGet]
        public async Task<IEnumerable<ThreadDto>> Get([FromQuery] SearchThreadsDto searchThreadsDto)
        {
            var res = _forumContext.Threads
                .Include(thread => thread.User)
                .Include(thread => thread.Posts)
                .ThenInclude(post => post.User)
                .Where(thread =>
                    (string.IsNullOrWhiteSpace(searchThreadsDto.SearchText) ||
                     (thread.Title.ToLower() + thread.Content.ToLower()).Contains(searchThreadsDto.SearchText.ToLower())) &&
                    (!searchThreadsDto.CanGetWithoutPosts || thread.Posts.Count == 0) &&
                    (searchThreadsDto.TagId == default || searchThreadsDto.TagId == 1 || thread.TagId == searchThreadsDto.TagId));

            if (searchThreadsDto.CanGetPopular)
                res = res.OrderByDescending(thread => thread.SeenCount);

            return (await res.ToListAsync())
                .Select(CreateThreadDto)
                .ToList();
        }

        [HttpGet("{id:int}")]
        public async Task<ThreadDetailDto> Get(int id)
        {
            var res = await _forumContext.Threads
                .Include(thread => thread.User)
                .Include(thread => thread.Posts)
                    .ThenInclude(post => post.User)
                .Include(thread => thread.Posts)
                    .ThenInclude(post => post.Likes)
                .FirstOrDefaultAsync(thread => thread.Id == id);

            res.SeenCount += 1;
            await _forumContext.SaveChangesAsync();
            
            return new ThreadDetailDto()
            {
                Thread = CreateThreadDto(res),
                Posts = res.Posts.Select(post => new PostDto()
                {
                    Id = post.Id,
                    Message = post.Message,
                    User = new UserDto()
                    {
                        Id = post.User.Id,
                        Login = post.User.Login,
                        PostCount = post.User.PostCount
                    },
                    ThreadId = post.Thread.Id,
                    LikeCount = post.Likes.Count,
                    ReplyTime = _forumHelper.GetRelativeDate(post.DateAdded)
                }).ToList()
            };
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ThreadDto thread)
        {
            if (!ModelState.IsValid || thread.TagId == default)
                return BadRequest();
            
            var toDb = new Thread()
            {
                Title = thread.Title,
                Content = thread.Content,
                UserId = thread.User.Id,
                DateAdded = DateTime.Now,
                TagId = thread.TagId
            };

            await _forumContext.Threads.AddAsync(toDb);
            await _forumContext.SaveChangesAsync();

            thread.Id = toDb.Id;

            return CreatedAtAction(nameof(Get), new {id = toDb.Id}, thread);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var fromDb = await _forumContext.Threads.Include(thread => thread.Posts)
                .FirstOrDefaultAsync(thread => thread.Id == id);
            _forumContext.Threads.Remove(fromDb);
            await _forumContext.SaveChangesAsync();

            return Ok();
        }
        
        private ThreadDto CreateThreadDto(Thread thread)
        {
            var lastPost = thread.Posts.LastOrDefault();

            return new ThreadDto()
            {
                Id = thread.Id,
                Content = thread.Content,
                Title = thread.Title,
                User = new UserDto()
                {
                    Id = thread.User.Id,
                    Login = thread.User.Login
                },
                PostCount = thread.Posts.Count,
                SeenCount = thread.SeenCount,
                LastReply = lastPost == null ? null : _forumHelper.GetRelativeDate(lastPost.DateAdded),
                LastReplyUser = lastPost == null
                    ? null
                    : new UserDto()
                    {
                        Login = thread.Posts.LastOrDefault()?.User.Login
                    }
            };
        }
    }
}