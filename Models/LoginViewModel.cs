using System.ComponentModel.DataAnnotations;

namespace QLTimViecLamSinhVien.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        // Thêm thuộc tính ReturnUrl để quay lại trang trước khi đăng nhập
        public string ReturnUrl { get; set; }
    }
}
