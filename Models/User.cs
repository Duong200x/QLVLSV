using System;
using System.Collections.Generic;

namespace QLTimViecLamSinhVien.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<Employer> Employers { get; set; } = new List<Employer>();

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
