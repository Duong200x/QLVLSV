using System;
using System.Collections.Generic;

namespace QLTimViecLamSinhVien.Models;

public partial class Employer
{
    public int EmployerId { get; set; }

    public string? CompanyName { get; set; }

    public string? CompanyDescription { get; set; }

    public string? ContactInfo { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
