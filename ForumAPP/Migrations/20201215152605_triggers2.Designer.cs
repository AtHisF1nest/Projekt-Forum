﻿// <auto-generated />
using System;
using ForumAPP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ForumAPP.Migrations
{
    [DbContext(typeof(ForumContext))]
    [Migration("20201215152605_triggers2")]
    partial class triggers2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("ForumAPP.Data.Models.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("LikedPostId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("LikedPostId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("ForumAPP.Data.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Message")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<int>("ThreadId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ThreadId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("ForumAPP.Data.Models.PostsArchive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateArchived")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Message")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<int>("ThreadId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("PostsArchives");
                });

            modelBuilder.Entity("ForumAPP.Data.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Color")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Color = "gray",
                            Name = "Brak oznaczenia"
                        });
                });

            modelBuilder.Entity("ForumAPP.Data.Models.Thread", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Content")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("SeenCount")
                        .HasColumnType("integer");

                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.HasIndex("UserId");

                    b.ToTable("Threads");
                });

            modelBuilder.Entity("ForumAPP.Data.Models.ThreadsArchive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Content")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateArchived")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("SeenCount")
                        .HasColumnType("integer");

                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ThreadsArchives");
                });

            modelBuilder.Entity("ForumAPP.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Login")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<int>("PostCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ForumAPP.Data.Models.UserPasswordsArchive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("DateArchived")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserPasswordsArchives");
                });

            modelBuilder.Entity("ForumAPP.Data.Models.Like", b =>
                {
                    b.HasOne("ForumAPP.Data.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ForumAPP.Data.Models.Post", "LikedPost")
                        .WithMany("Likes")
                        .HasForeignKey("LikedPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("LikedPost");
                });

            modelBuilder.Entity("ForumAPP.Data.Models.Post", b =>
                {
                    b.HasOne("ForumAPP.Data.Models.Thread", "Thread")
                        .WithMany("Posts")
                        .HasForeignKey("ThreadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ForumAPP.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Thread");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ForumAPP.Data.Models.Thread", b =>
                {
                    b.HasOne("ForumAPP.Data.Models.Tag", "Tag")
                        .WithMany("Threads")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ForumAPP.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ForumAPP.Data.Models.UserPasswordsArchive", b =>
                {
                    b.HasOne("ForumAPP.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ForumAPP.Data.Models.Post", b =>
                {
                    b.Navigation("Likes");
                });

            modelBuilder.Entity("ForumAPP.Data.Models.Tag", b =>
                {
                    b.Navigation("Threads");
                });

            modelBuilder.Entity("ForumAPP.Data.Models.Thread", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
