using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QLTimViecLamSinhVien.Models;
using QLTimViecLamSinhVien.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QLTimViecLamSinhVien.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Action hiển thị trang chủ
        public IActionResult Index()
        {
            return View();
        }

        // Action xử lý tìm kiếm công việc
        [HttpGet]
        public IActionResult Search(string keyword)
        {
            // Tìm kiếm công việc trong cơ sở dữ liệu dựa trên từ khóa
            var jobs = _context.Jobs.AsQueryable();

            // Tìm kiếm theo từ khóa
            if (!string.IsNullOrEmpty(keyword))
            {
                jobs = jobs.Where(j => j.Title.Contains(keyword) || j.CompanyName.Contains(keyword));
            }

            // Trả về view Index với danh sách công việc tìm được
            return View("Index", jobs.ToList());
        }

        // Đăng ký - GET để hiển thị form đăng ký
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Đăng ký - POST để xử lý đăng ký người dùng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Tạo người dùng mới
                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    FullName = model.FullName
                };

                // Hash mật khẩu trước khi lưu
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Xác nhận email nếu cần
                    user.EmailConfirmed = true; // Đặt EmailConfirmed thành true nếu bạn không sử dụng xác minh email

                    // Đăng nhập người dùng ngay sau khi tạo thành công
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Hiển thị lỗi nếu có
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // Đăng nhập
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng.");
            }

            return View(model);
        }
    }
}
