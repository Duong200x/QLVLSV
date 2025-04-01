using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTimViecLamSinhVien.Models
{
    [Table("jobs")] // Ánh xạ tới bảng "jobs" trong DB
    public partial class Job
    {
        [Key]
        [Column("job_id")]
        public int JobId { get; set; }

        [Column("title")]
        public string Title { get; set; } = null!;

        [Column("description")]
        public string? Description { get; set; }

        [Column("requirements")]
        public string? Requirements { get; set; }

        [Column("company_name")]
        public string? CompanyName { get; set; }

        [Column("location")]
        public string? Location { get; set; }

        [Column("salary")]
        public decimal? Salary { get; set; }

        [Column("posted_by")]
        public int? PostedBy { get; set; }

        [Column("posted_at")]
        public DateTime? PostedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation property tới User (nếu "user_id" là PK của User)
        [ForeignKey(nameof(PostedBy))]
        public virtual User? PostedByNavigation { get; set; }

        // Quan hệ 1-nhiều (mỗi Job có nhiều Application)
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

        // Quan hệ nhiều-nhiều với JobCategory qua bảng job_category_link
        public virtual ICollection<JobCategory> Categories { get; set; } = new List<JobCategory>();
    }
}
