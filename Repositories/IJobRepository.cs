using QLTimViecLamSinhVien.Models;
using System.Collections.Generic;

namespace QLTimViecLamSinhVien.Repositories
{
    public interface IJobRepository
    {
        IEnumerable<Job> GetAllJobs();
        Job GetJobById(int id);
        void AddJob(Job job);
        void UpdateJob(Job job);
        void DeleteJob(int id);
        void Save();
    }
}
