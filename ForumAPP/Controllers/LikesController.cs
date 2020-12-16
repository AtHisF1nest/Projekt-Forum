using System;
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
    public class LikesController : ControllerBase
    {
        private readonly ForumContext _forumContext;

        public LikesController(ForumContext forumContext)
        {
            _forumContext = forumContext;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LikeDto like)
        {
            var toDb = new Like()
            {
                AuthorId = like.AuthorId,
                LikedPostId = like.LikedPostId,
                DateAdded = DateTime.Now
            };

            var existing = await _forumContext.Likes.FirstOrDefaultAsync(like1 =>
                like1.AuthorId == like.AuthorId && like1.LikedPostId == like.LikedPostId);

            if (existing != null)
            {
                like.Id = 0;
                _forumContext.Likes.Remove(existing);
                await _forumContext.SaveChangesAsync();
            }
            else
            {
                await _forumContext.Likes.AddAsync(toDb);
                await _forumContext.SaveChangesAsync();
                like.Id = toDb.Id;
            }

            return Ok(like);
        }
    }
}