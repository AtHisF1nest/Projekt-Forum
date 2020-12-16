using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumAPP.Data;
using ForumAPP.Data.Models;
using ForumAPP.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumAPP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly ForumContext _forumContext;

        public PostsController(ForumContext forumContext)
        {
            _forumContext = forumContext;
        }
        
        [HttpGet]
        public async Task<IEnumerable<PostDto>> Get(int threadId)
        {
            var res = await _forumContext.Posts.Include(post => post.Likes)
                .Include(post => post.User)
                .Include(post => post.Thread)
                .Where(post => post.Thread.Id == threadId)
                .Select(post => new PostDto()
                {
                    Id = post.Id,
                    Message = post.Message,
                    LikeCount = post.Likes.Count,
                    ReplyTime = post.DateAdded.ToShortTimeString(),
                    ThreadId = post.Thread.Id,
                    User = new UserDto()
                    {
                        Id = post.User.Id,
                        Login = post.User.Login
                    }
                })
                .ToListAsync();

            return res;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostDto post)
        {
            var toDb = new Post()
            {
                Message = post.Message,
                ThreadId = post.ThreadId,
                UserId = post.User.Id,
                DateAdded = DateTime.Now
            };

            await _forumContext.Posts.AddAsync(toDb);
            await _forumContext.SaveChangesAsync();

            post.Id = toDb.Id;

            return Ok(post);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var fromDb = await _forumContext.Posts.Include(post => post.Likes)
                .FirstOrDefaultAsync(post => post.Id == id);
            _forumContext.Posts.Remove(fromDb);
            await _forumContext.SaveChangesAsync();

            return Ok();
        }
    }
}