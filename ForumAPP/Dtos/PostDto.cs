using System;

namespace ForumAPP.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public int ThreadId { get; set; }
        public string ReplyTime { get; set; }
        public string Message { get; set; }
        public int LikeCount { get; set; }
        public UserDto User { get; set; }
    }
}