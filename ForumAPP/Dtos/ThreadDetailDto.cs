using System.Collections.Generic;

namespace ForumAPP.Dtos
{
    public class ThreadDetailDto
    {
        public ThreadDto Thread { get; set; }
        public List<PostDto> Posts { get; set; }
    }
}