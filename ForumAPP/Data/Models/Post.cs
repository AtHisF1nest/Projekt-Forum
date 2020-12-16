using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumAPP.Data.Models
{
    [Serializable]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        [MinLength(3)]
        [MaxLength(2000)]
        public string Message { get; set; }
        public Thread Thread { get; set; }
        [ForeignKey(nameof(Thread))]
        public int ThreadId { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public List<Like> Likes { get; set; }
            = new List<Like>();
    }
}