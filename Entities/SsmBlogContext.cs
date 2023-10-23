using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace efcore.Entities;

public partial class SsmBlogContext : DbContext
{
    public SsmBlogContext()
    {
    }

    public SsmBlogContext(DbContextOptions<SsmBlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Avatar> Avatars { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;database=SSM_BLOG;uid=root;pwd=123456", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("article");

            entity.HasIndex(e => e.AuthorId, "author_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Content)
                .HasColumnType("mediumtext")
                .HasColumnName("content");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("date_created");
            entity.Property(e => e.Title)
                .HasMaxLength(256)
                .HasColumnName("title");

            entity.HasOne(d => d.Author).WithMany(p => p.Articles)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("article_ibfk_1");
        });

        modelBuilder.Entity<Avatar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("avatar");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsPredefined).HasColumnName("is_predefined");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("comment");

            entity.HasIndex(e => e.ArticleId, "article_id");

            entity.HasIndex(e => e.AuthorId, "author_id");

            entity.HasIndex(e => e.ParentId, "parent_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArticleId).HasColumnName("article_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Content)
                .HasColumnType("mediumtext")
                .HasColumnName("content");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("date_created");
            entity.Property(e => e.Level)
                .HasDefaultValueSql("'0'")
                .HasColumnName("level");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");

            entity.HasOne(d => d.Article).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_ibfk_2");

            entity.HasOne(d => d.Author).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_ibfk_1");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("comment_ibfk_3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.AvatarId, "avatar_id");

            entity.HasIndex(e => e.DetailId, "detail_id");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AvatarId)
                .HasDefaultValueSql("'1'")
                .HasColumnName("avatar_id");
            entity.Property(e => e.DetailId).HasColumnName("detail_id");
            entity.Property(e => e.Pwd)
                .HasMaxLength(32)
                .HasColumnName("pwd");
            entity.Property(e => e.Username)
                .HasMaxLength(128)
                .HasColumnName("username");

            entity.HasOne(d => d.Avatar).WithMany(p => p.Users)
                .HasForeignKey(d => d.AvatarId)
                .HasConstraintName("user_ibfk_1");

            entity.HasOne(d => d.Detail).WithMany(p => p.Users)
                .HasForeignKey(d => d.DetailId)
                .HasConstraintName("user_ibfk_2");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_detail");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateBirth).HasColumnName("date_birth");
            entity.Property(e => e.Descrip)
                .HasColumnType("text")
                .HasColumnName("descrip");
            entity.Property(e => e.Fname)
                .HasMaxLength(128)
                .HasColumnName("fname");
            entity.Property(e => e.Lname)
                .HasMaxLength(128)
                .HasColumnName("lname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
