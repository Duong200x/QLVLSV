using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLTimViecLamSinhVien.Models;

public partial class QLTimViecLamSinhVienContext : DbContext
{
    public QLTimViecLamSinhVienContext()
    {
    }

    public QLTimViecLamSinhVienContext(DbContextOptions<QLTimViecLamSinhVienContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Employer> Employers { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobCategory> JobCategories { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:QLViecLamSinhVien");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__applicat__3BCBDCF240597C18");

            entity.ToTable("applications");

            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.AppliedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("applied_at");
            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasDefaultValue("submitted")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Job).WithMany(p => p.Applications)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK__applicati__job_i__1DE57479");

            entity.HasOne(d => d.User).WithMany(p => p.Applications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__applicati__user___1ED998B2");
        });

        modelBuilder.Entity<Employer>(entity =>
        {
            entity.HasKey(e => e.EmployerId).HasName("PK__employer__365FA4E7F048706A");

            entity.ToTable("employers");

            entity.Property(e => e.EmployerId).HasColumnName("employer_id");
            entity.Property(e => e.CompanyDescription)
                .HasColumnType("text")
                .HasColumnName("company_description");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("company_name");
            entity.Property(e => e.ContactInfo)
                .HasColumnType("text")
                .HasColumnName("contact_info");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Employers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__employers__user___21B6055D");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__jobs__6E32B6A5516F566F");

            entity.ToTable("jobs");

            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("company_name");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.PostedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("posted_at");
            entity.Property(e => e.PostedBy).HasColumnName("posted_by");
            entity.Property(e => e.Requirements)
                .HasColumnType("text")
                .HasColumnName("requirements");
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salary");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.PostedByNavigation).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.PostedBy)
                .HasConstraintName("FK__jobs__posted_by__173876EA");

            entity.HasMany(d => d.Categories).WithMany(p => p.Jobs)
                .UsingEntity<Dictionary<string, object>>(
                    "JobCategoryLink",
                    r => r.HasOne<JobCategory>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__job_categ__categ__276EDEB3"),
                    l => l.HasOne<Job>().WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__job_categ__job_i__267ABA7A"),
                    j =>
                    {
                        j.HasKey("JobId", "CategoryId").HasName("PK__job_cate__3366583EF68D5835");
                        j.ToTable("job_category_link");
                        j.IndexerProperty<int>("JobId").HasColumnName("job_id");
                        j.IndexerProperty<int>("CategoryId").HasColumnName("category_id");
                    });
        });

        modelBuilder.Entity<JobCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__job_cate__D54EE9B41FF32FE2");

            entity.ToTable("job_categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(e => e.PageId).HasName("PK__pages__637F305A9FC51C95");

            entity.ToTable("pages");

            entity.Property(e => e.PageId).HasColumnName("page_id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PageName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("page_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F18BAD3E5");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
