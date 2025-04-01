using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using QLTimViecLamSinhVien.Models;

namespace QLTimViecLamSinhVien.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
                          UserManager<ApplicationUser> userManager,
                          ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/"); // Nếu không có URL trả về, chuyển hướng về trang chính

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Người dùng đã đăng nhập thành công.");
                    return LocalRedirect(returnUrl); // Chuyển hướng về URL đích sau khi đăng nhập
                }
                else
                {
                    TempData["ErrorMessage"] = "Đăng nhập không thành công. Vui lòng kiểm tra lại thông tin đăng nhập."; // Thông báo lỗi
                    return RedirectToPage(); // Quay lại trang đăng nhập
                }
            }

            return Page();
        }
    }
}
