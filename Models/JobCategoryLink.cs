using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTimViecLamSinhVien.Models
{
    [Table("job_category_link")]
    public partial class JobCategoryLink
    {
        [Key, Column("job_id", Order = 1)]
        public int JobId { get; set; }

        [Key, Column("category_id", Order = 2)]
        public int CategoryId { get; set; }

        // Khoá ngoại đến Job
        [ForeignKey(nameof(JobId))]
        public virtual Job Job { get; set; }

        // Khoá ngoại đến JobCategory
        [ForeignKey(nameof(CategoryId))]
        public virtual JobCategory JobCategory { get; set; }
    }
}
