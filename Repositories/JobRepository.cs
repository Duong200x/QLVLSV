using QLTimViecLamSinhVien.Models;
using QLTimViecLamSinhVien.Data;
using System.Collections.Generic;
using System.Linq;

namespace QLTimViecLamSinhVien.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext _context;

        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Phương thức lấy tất cả công việc từ cơ sở dữ liệu
        public IEnumerable<Job> GetAllJobs()
        {
            return _context.Jobs.ToList();  // Chỉ truy vấn các trường có sẵn trong bảng Jobs
        }

        public Job GetJobById(int id)
        {
            return _context.Jobs.FirstOrDefault(j => j.JobId == id);
        }

        public void AddJob(Job job)
        {
            _context.Jobs.Add(job);
        }

        public void UpdateJob(Job job)
        {
            _context.Jobs.Update(job);
        }

        public void DeleteJob(int id)
        {
            var job = _context.Jobs.FirstOrDefault(j => j.JobId == id);
            if (job != null)
            {
                _context.Jobs.Remove(job);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
