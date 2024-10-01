using System;
using System.Collections.Generic;

namespace easyUTR.Models;

public partial class ItemsInOrder
{
    public int ItemId { get; set; }

    public int OrderId { get; set; }

    public int StoreId { get; set; }

    public int NumberOf { get; set; }

    public decimal TotalItemCost { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual CustomerOrder Order { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
