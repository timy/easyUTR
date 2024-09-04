using System;
using System.Collections.Generic;

namespace easyUTR.Models;

public partial class Job
{
    public int JobId { get; set; }

    public string JobName { get; set; } = null!;

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
