using System;
using System.Collections.Generic;

namespace easyUTR.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string SupplierDescription { get; set; } = null!;

    public string? SupplierUrl { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
