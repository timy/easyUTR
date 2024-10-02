using System;
using System.Collections.Generic;

namespace easyUTR.Models;

public partial class Staff
{
    public string StaffId { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int StoreId { get; set; }

    public int JobId { get; set; }

    public int? JobLevel { get; set; }

    public virtual Job Job { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
