using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using QLTimViecLamSinhVien.Data;
using QLTimViecLamSinhVien.Models;
using QLTimViecLamSinhVien.Repositories;
using QLTimViecLamSinhVien.Services;  // Thêm namespace cho EmailSender

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext với chuỗi kết nối trong appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cấu hình Identity để sử dụng ApplicationUser (hoặc CustomUser)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Đăng ký dịch vụ gửi email
builder.Services.AddTransient<IEmailSender, EmailSender>(); // Đảm bảo dịch vụ này được đăng ký

// Thêm các dịch vụ MVC và Controllers/Views
builder.Services.AddControllersWithViews();

// Đăng ký các repository
builder.Services.AddScoped<IJobRepository, JobRepository>();

// Thêm dịch vụ Razor Pages
builder.Services.AddRazorPages();  // Đảm bảo thêm Razor Pages vào DI

var app = builder.Build();

// Cấu hình môi trường và các middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Hiển thị trang lỗi cho môi trường sản xuất
    app.UseHsts(); // Cấu hình HSTS cho bảo mật HTTP
}

app.UseHttpsRedirection(); // Chuyển hướng tất cả yêu cầu HTTP sang HTTPS
app.UseStaticFiles(); // Cho phép sử dụng các tệp tĩnh (CSS, JS, hình ảnh)

app.UseRouting();
app.UseAuthentication(); // Sử dụng tính năng xác thực
app.UseAuthorization();  // Sử dụng tính năng phân quyền

// Cấu hình các route cho controllers và Identity
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages(); // Đảm bảo các trang Razor Pages (login, register) được cấu hình

app.Run();
