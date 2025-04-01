using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTimViecLamSinhVien.Models
{
    [Table("job_categories")]
    public partial class JobCategory
    {
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("category_name")]
        public string CategoryName { get; set; }

        // Thêm thuộc tính này để EF biết mỗi Category có nhiều Jobs
        public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
