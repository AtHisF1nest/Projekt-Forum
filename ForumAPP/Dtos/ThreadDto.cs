using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumAPP.Dtos
{
    public class ThreadDto
    {
        public int Id { get; set; }
        [MinLength(3)]
        public string Title { get; set; }
        public string Content { get; set; }
        public int SeenCount { get; set; }
        public string LastReply { get; set; }
        public UserDto User { get; set; }
        public int PostCount { get; set; }
        public UserDto LastReplyUser { get; set; }
        public int TagId { get; set; }
    }
}