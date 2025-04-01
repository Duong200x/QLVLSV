using Microsoft.AspNetCore.Identity;

namespace QLTimViecLamSinhVien.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
