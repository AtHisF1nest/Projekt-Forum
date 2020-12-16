using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumAPP.Data.Models
{
    public class UserPasswordsArchive
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public string Login { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateArchived { get; set; }
    }
}