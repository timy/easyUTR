using System;
using System.Collections.Generic;

namespace easyUTR.Models;

public partial class ItemCategory
{
    public int CategoryId { get; set; }

    public int? ParentCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
