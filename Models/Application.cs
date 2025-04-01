using System;
using System.Collections.Generic;

namespace QLTimViecLamSinhVien.Models;

public partial class Application
{
    public int ApplicationId { get; set; }

    public int? JobId { get; set; }

    public int? UserId { get; set; }

    public string? Status { get; set; }

    public DateTime? AppliedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Job? Job { get; set; }

    public virtual User? User { get; set; }
}
