using Microsoft.AspNetCore.Mvc;
using QLTimViecLamSinhVien.Models;
using QLTimViecLamSinhVien.Repositories;

namespace QLTimViecLamSinhVien.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobRepository _jobRepository;

        public JobsController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        // GET: Jobs
        public IActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            var jobs = _jobRepository.GetAllJobs();  // Gọi phương thức từ repository để lấy danh sách công việc
            return View(jobs);  // Trả về view với danh sách công việc
        }

        // GET: Jobs/Details/5
        public IActionResult Details(int id)
        {
            var job = _jobRepository.GetJobById(id);  // Lấy công việc theo ID từ repository
            if (job == null)
            {
                return NotFound();  // Nếu không tìm thấy công việc, trả về lỗi 404
            }
            return View(job);  // Trả về view chi tiết công việc
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            return View();  // Trả về view để tạo công việc mới
        }

        // POST: Jobs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,Requirements,CompanyName,Location,Salary,PostedBy")] Job job)
        {
            if (ModelState.IsValid)
            {
                job.PostedAt = DateTime.Now;
                job.UpdatedAt = DateTime.Now;

                _jobRepository.AddJob(job);  // Thêm công việc mới vào repository
                _jobRepository.Save();  // Lưu vào cơ sở dữ liệu
                return RedirectToAction(nameof(Index));  // Chuyển hướng về danh sách công việc
            }
            return View(job);  // Nếu không hợp lệ, trả về view tạo công việc mới
        }

        // GET: Jobs/Edit/5
        public IActionResult Edit(int id)
        {
            var job = _jobRepository.GetJobById(id);  // Lấy công việc cần chỉnh sửa từ repository
            if (job == null)
            {
                return NotFound();  // Nếu không tìm thấy công việc, trả về lỗi 404
            }
            return View(job);  // Trả về view chỉnh sửa công việc
        }

        // POST: Jobs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("JobId,Title,Description,Requirements,CompanyName,Location,Salary,PostedBy,PostedAt")] Job job)
        {
            if (id != job.JobId)
            {
                return NotFound();  // Nếu ID không trùng khớp, trả về lỗi 404
            }

            if (ModelState.IsValid)
            {
                job.UpdatedAt = DateTime.Now;

                _jobRepository.UpdateJob(job);  // Cập nhật công việc trong repository
                _jobRepository.Save();  // Lưu thay đổi vào cơ sở dữ liệu
                return RedirectToAction(nameof(Index));  // Chuyển hướng về danh sách công việc
            }
            return View(job);  // Nếu không hợp lệ, trả về view chỉnh sửa công việc
        }

        // POST: Jobs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _jobRepository.DeleteJob(id);  // Xóa công việc theo ID từ repository
            _jobRepository.Save();  // Lưu thay đổi vào cơ sở dữ liệu

            TempData["Message"] = "Đã xóa thành công!";  // Thông báo sau khi xóa
            return RedirectToAction(nameof(Index));  // Chuyển hướng về danh sách công việc
        }
    }
}
