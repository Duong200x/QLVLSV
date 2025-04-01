using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QLTimViecLamSinhVien.Models;

namespace QLTimViecLamSinhVien.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Bảng jobs
        public DbSet<Job> Jobs { get; set; }

        // Bảng job_categories
        public DbSet<JobCategory> JobCategories { get; set; }

        // Bảng job_category_link (bảng trung gian)
        public DbSet<JobCategoryLink> JobCategoryLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Khai báo bảng trung gian có composite key (job_id, category_id)
            modelBuilder.Entity<JobCategoryLink>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.CategoryId });
                entity.ToTable("job_category_link");
            });

            // (Tuỳ chọn) Thiết lập quan hệ nhiều-nhiều bằng UsingEntity
            // Giúp EF hiểu: 1 Job có nhiều Category, 1 Category có nhiều Job
            // qua bảng job_category_link
            modelBuilder.Entity<Job>()
                .HasMany(j => j.Categories)
                .WithMany(c => c.Jobs)
                .UsingEntity<JobCategoryLink>(
                    link => link
                        .HasOne(x => x.JobCategory)
                        .WithMany()               // Hoặc .WithMany(x => x.JobCategoryLinks)
                        .HasForeignKey(x => x.CategoryId),

                    link => link
                        .HasOne(x => x.Job)
                        .WithMany()               // Hoặc .WithMany(x => x.JobCategoryLinks)
                        .HasForeignKey(x => x.JobId),

                    link =>
                    {
                        link.ToTable("job_category_link");
                        link.HasKey(e => new { e.JobId, e.CategoryId });
                    }
                );

            // Nếu bảng "jobs" và "job_categories" có cột PK là job_id, category_id
            // và bạn muốn chắc chắn EF map đúng, có thể thêm:
            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("jobs");
                entity.HasKey(e => e.JobId);
            });

            modelBuilder.Entity<JobCategory>(entity =>
            {
                entity.ToTable("job_categories");
                entity.HasKey(e => e.CategoryId);
            });
        }
    }
}
