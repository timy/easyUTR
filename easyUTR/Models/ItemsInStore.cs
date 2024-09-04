using System;
using System.Collections.Generic;

namespace easyUTR.Models;

public partial class ItemsInStore
{
    public int ItemId { get; set; }

    public int StoreId { get; set; }

    public decimal Price { get; set; }

    public int NumberInStock { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
