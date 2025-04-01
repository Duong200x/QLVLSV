using System;
using System.Collections.Generic;

namespace QLTimViecLamSinhVien.Models;

public partial class Page
{
    public int PageId { get; set; }

    public string? PageName { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
