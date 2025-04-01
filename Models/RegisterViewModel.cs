using System.ComponentModel.DataAnnotations;

namespace QLTimViecLamSinhVien.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự.")]
        public string FullName { get; set; }  // Tên người dùng

        [Required]
        [StringLength(100, ErrorMessage = "Tên đăng nhập phải có ít nhất 6 ký tự.", MinimumLength = 6)]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }  // Tên đăng nhập

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }

        // Thêm thuộc tính ReturnUrl để quay lại trang trước khi đăng ký
        public string ReturnUrl { get; set; }
    }
}
