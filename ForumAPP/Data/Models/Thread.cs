using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumAPP.Data.Models
{
    [Serializable]
    public class Thread
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MinLength(10)]
        [MaxLength(80)]
        public string Title { get; set; }
        [MinLength(3)]
        [MaxLength(2000)]
        public string Content { get; set; }
        public int SeenCount { get; set; }
        public DateTime DateAdded { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public List<Post> Posts { get; set; }
        public Tag Tag { get; set; }
        [ForeignKey(nameof(Tag))]
        public int TagId { get; set; }
    }
}