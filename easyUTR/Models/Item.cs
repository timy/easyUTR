using System;
using System.Collections.Generic;

namespace easyUTR.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public string ItemDescription { get; set; } = null!;

    public string? ItemImage { get; set; }

    public int CategoryId { get; set; }

    public int SupplierId { get; set; }

    public virtual ItemCategory? Category { get; set; } = null!;

    public virtual ICollection<ItemsInOrder> ItemsInOrders { get; set; } = new List<ItemsInOrder>();

    public virtual ICollection<ItemsInStore> ItemsInStores { get; set; } = new List<ItemsInStore>();

    public virtual Supplier? Supplier { get; set; } = null!;
}
