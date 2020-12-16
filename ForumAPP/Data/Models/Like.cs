using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumAPP.Data.Models
{
    [Serializable]
    public class Like
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public User Author { get; set; }
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
        public Post LikedPost { get; set; }
        [ForeignKey(nameof(LikedPost))]
        public int LikedPostId { get; set; }
    }
}