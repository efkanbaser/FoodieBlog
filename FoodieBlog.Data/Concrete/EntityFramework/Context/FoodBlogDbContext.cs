using System;
using System.Collections.Generic;
using FoodieBlog.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace FoodieBlog.Data.Concrete.EntityFramework.Context;

public partial class FoodBlogDbContext : DbContext
{
    public FoodBlogDbContext()
    {
    }

    public FoodBlogDbContext(DbContextOptions<FoodBlogDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminMenu> AdminMenus { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Interaction> Interactions { get; set; }

    public virtual DbSet<MenuAuthorization> MenuAuthorizations { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostTag> PostTags { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=EFKO\\SQLEXPRESS;database=FoodBlogDB;trusted_connection=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminMenu>(entity =>
        {
            entity.ToTable("AdminMenu");

            entity.HasIndex(e => e.ParentMenuId, "IX_AdminMenu_ParentMenuId");

            entity.Property(e => e.Header).HasMaxLength(50);
            entity.Property(e => e.MenuIcon).HasMaxLength(200);
            entity.Property(e => e.Url).HasMaxLength(200);

            entity.HasOne(d => d.ParentMenu).WithMany(p => p.InverseParentMenu)
                .HasForeignKey(d => d.ParentMenuId)
                .HasConstraintName("FK_AdminMenu_AdminMenu");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasIndex(e => e.PostId, "IX_Comments_PostId");

            entity.HasIndex(e => e.UserId, "IX_Comments_UserId");

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
            entity.HasIndex(e => e.PostId, "IX_Interactions_PostId");

            entity.HasIndex(e => e.UserId, "IX_Interactions_UserId");

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

        modelBuilder.Entity<MenuAuthorization>(entity =>
        {
            entity.ToTable("MenuAuthorization");

            entity.HasOne(d => d.Menu).WithMany(p => p.MenuAuthorizations)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("FK_MenuAuthorization_AdminMenu");

            entity.HasOne(d => d.Role).WithMany(p => p.MenuAuthorizations)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_MenuAuthorization_Role");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Posts_UserId");

            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Contents).HasColumnType("text");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Posts_Users");
        });

        modelBuilder.Entity<PostTag>(entity =>
        {
            entity.HasIndex(e => e.PostId, "IX_PostTags_PostId");

            entity.HasIndex(e => e.TagId, "IX_PostTags_TagId");

            entity.HasOne(d => d.Post).WithMany(p => p.PostTags)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_PostTags_Posts");

            entity.HasOne(d => d.Tag).WithMany(p => p.PostTags)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("FK_PostTags_Tags");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleName).HasMaxLength(50);
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

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRole");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_UserRole_Role");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserRole_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
