using System;

namespace ForumAPP.Dtos
{
    public class LikeDto
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int LikedPostId { get; set; }
    }
}