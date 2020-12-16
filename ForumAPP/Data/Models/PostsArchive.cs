using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumAPP.Data.Models
{
    public class PostsArchive
    {
        public DateTime DateArchived { get; set; }
        [Key]
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        [MinLength(3)]
        [MaxLength(2000)]
        public string Message { get; set; }
        public int ThreadId { get; set; }
        public int UserId { get; set; }
    }
}