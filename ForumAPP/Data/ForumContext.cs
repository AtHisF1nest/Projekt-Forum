using ForumAPP.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumAPP.Data
{
    public class ForumContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
            => builder.UseNpgsql("Host=localhost;Database=ForumDB;Username=ForumUser;Password=ForumPassword");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseIdentityColumns();
            builder.Entity<Tag>().HasData(new Tag() {Id = 1, Name = "Wszystkie", Color = "gray"},
                new Tag() {Id = 2, Name = "Brak oznaczenia", Color = "gray"});
        }

        #region Tables

        public DbSet<User> Users { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<ThreadsArchive> ThreadsArchives { get; set; }
        public DbSet<PostsArchive> PostsArchives { get; set; }
        public DbSet<UserPasswordsArchive> UserPasswordsArchives { get; set; }

        #endregion
    }
}