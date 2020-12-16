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
    public class TagController : ControllerBase
    {
        private readonly ForumContext _forumContext;

        public TagController(ForumContext forumContext)
        {
            _forumContext = forumContext;
        }

        [HttpGet]
        public async Task<IEnumerable<TagDto>> Get()
        {
            return await _forumContext.Tags.Select(tag => new TagDto()
            {
                Color = tag.Color,
                Id = tag.Id,
                Name = tag.Name
            }).ToListAsync();
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TagDto tag)
        {
            var toDb = new Tag()
            {
                Color = tag.Color,
                Name = tag.Name
            };

            var existing =
                await _forumContext.Tags.FirstOrDefaultAsync(tag1 => tag1.Name.ToLower() == tag.Name.ToLower());

            if (existing != null)
            {
                return BadRequest();
            }

            await _forumContext.Tags.AddAsync(toDb);
            await _forumContext.SaveChangesAsync();
            tag.Id = toDb.Id;

            return Ok(tag);
        }
    }
}