using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Prak.Models;

public partial class PraktikaContext : DbContext
{
    public PraktikaContext()
    {
    }

    public PraktikaContext(DbContextOptions<PraktikaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TAZIK;Database=Praktika;Trusted_Connection=True;MultipleActiveResultSets=False;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Answers__99FC143B01CE94F2");

            entity.Property(e => e.CommentId)
                .ValueGeneratedNever()
                .HasColumnName("Comment_ID");
            entity.Property(e => e.Content).HasMaxLength(50);
            entity.Property(e => e.NegativeRatings).HasColumnName("Negative_Ratings");
            entity.Property(e => e.PositiveRatings).HasColumnName("Positive_Ratings");
            entity.Property(e => e.UserIds).HasColumnName("User_IDs");

            entity.HasOne(d => d.Comment).WithOne(p => p.Answer)
                .HasForeignKey<Answer>(d => d.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Answers__Comment__4316F928");

            entity.HasOne(d => d.UserIdsNavigation).WithMany(p => p.Answers)
                .HasForeignKey(d => d.UserIds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Answers__User_ID__4222D4EF");
        });

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PK__Article__500A46B665E47F6B");

            entity.ToTable("Article");

            entity.Property(e => e.ArticleId)
                .ValueGeneratedNever()
                .HasColumnName("Article_ID");
            entity.Property(e => e.Content).HasMaxLength(50);
            entity.Property(e => e.DatePublished)
                .HasColumnType("datetime")
                .HasColumnName("Date_Published");
            entity.Property(e => e.NegativeRatings).HasColumnName("Negative_Ratings");
            entity.Property(e => e.PositiveRatings).HasColumnName("Positive_Ratings");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UserIds).HasColumnName("User_IDs");

            entity.HasOne(d => d.UserIdsNavigation).WithMany(p => p.Articles)
                .HasForeignKey(d => d.UserIds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Article__User_ID__3F466844");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__99FC143B3DAC6F40");

            entity.Property(e => e.CommentId)
                .ValueGeneratedNever()
                .HasColumnName("Comment_ID");
            entity.Property(e => e.Content).HasMaxLength(50);
            entity.Property(e => e.NegativeRatings).HasColumnName("Negative_Ratings");
            entity.Property(e => e.PositiveRatings).HasColumnName("Positive_Ratings");
            entity.Property(e => e.UserIds).HasColumnName("User_IDs");

            entity.HasOne(d => d.UserIdsNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserIds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Article");

            entity.HasOne(d => d.UserIds1).WithMany(p => p.CommentsNavigation)
                .HasForeignKey(d => d.UserIds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__User_I__3C69FB99");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleLvl).HasName("PK__Roles__4E35DEAF398B61B8");

            entity.Property(e => e.RoleLvl)
                .ValueGeneratedNever()
                .HasColumnName("Role_Lvl");
            entity.Property(e => e.Names).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserIds).HasName("PK__Users__A2378F67E0C23FF2");

            entity.Property(e => e.UserIds)
                .ValueGeneratedNever()
                .HasColumnName("User_IDs");
            entity.Property(e => e.Comments).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Logins).HasMaxLength(50);
            entity.Property(e => e.MadeArticles).HasColumnName("Made_Articles");
            entity.Property(e => e.Names).HasMaxLength(50);
            entity.Property(e => e.Passwords).HasMaxLength(50);
            entity.Property(e => e.ReadingCategory)
                .HasMaxLength(50)
                .HasColumnName("Reading_Category");
            entity.Property(e => e.RoleLvl).HasColumnName("Role_Lvl");

            entity.HasOne(d => d.RoleLvlNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleLvl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
