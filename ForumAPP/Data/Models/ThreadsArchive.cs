using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumAPP.Data.Models
{
    public class ThreadsArchive
    {
        public DateTime DateArchived { get; set; }
        [Key]
        public int Id { get; set; }
        [MinLength(10)]
        [MaxLength(80)]
        public string Title { get; set; }
        [MinLength(3)]
        [MaxLength(2000)]
        public string Content { get; set; }
        public int SeenCount { get; set; }
        public DateTime DateAdded { get; set; }
        public int UserId { get; set; }
        public int TagId { get; set; }
    }
}