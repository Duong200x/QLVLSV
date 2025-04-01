using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using QLTimViecLamSinhVien.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace QLTimViecLamSinhVien.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 6)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string FullName { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/"); // Nếu không có URL trả về, chuyển hướng về trang chính

            if (ModelState.IsValid) // Kiểm tra xem dữ liệu có hợp lệ không
            {
                // Tạo người dùng mới
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, FullName = Input.FullName };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded) // Nếu đăng ký thành công
                {
                    _logger.LogInformation("Người dùng đã đăng ký thành công.");

                    // Đăng nhập ngay sau khi đăng ký thành công
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // Gửi email xác nhận (nếu yêu cầu xác nhận email)
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Xác nhận email của bạn",
                        $"Vui lòng xác nhận tài khoản của bạn bằng cách <a href='{callbackUrl}'>nhấp vào đây</a>.");

                    return LocalRedirect(returnUrl); // Chuyển hướng về URL đích sau khi đăng ký thành công
                }
                else
                {
                    // Nếu có lỗi, thêm lỗi vào ModelState
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // Nếu Model không hợp lệ, quay lại trang đăng ký
            return Page();
        }
    }
}
