using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FoodieBlog.Model.Entity;

public partial class FoodBlogDbContext : DbContext
{
    public FoodBlogDbContext()
    {
    }

    public FoodBlogDbContext(DbContextOptions<FoodBlogDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Interaction> Interactions { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostTag> PostTags { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=EFKO\\SQLEXPRESS;database=FoodBlogDB;trusted_connection=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.Contents).HasColumnType("text");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_Comments_Posts");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Comments_Users");
        });

        modelBuilder.Entity<Interaction>(entity =>
        {
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Post).WithMany(p => p.Interactions)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_Interactions_Posts");

            entity.HasOne(d => d.User).WithMany(p => p.Interactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Interactions_Users");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Contents).HasColumnType("text");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Posts_Users");
        });

        modelBuilder.Entity<PostTag>(entity =>
        {
            entity.HasOne(d => d.Post).WithMany(p => p.PostTags)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_PostTags_Posts");

            entity.HasOne(d => d.Tag).WithMany(p => p.PostTags)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("FK_PostTags_Tags");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.Property(e => e.TagName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User");

            entity.Property(e => e.Bio).HasColumnType("text");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.ProfilePic).HasMaxLength(255);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
