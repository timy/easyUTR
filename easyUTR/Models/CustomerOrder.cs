using System;
using System.Collections.Generic;

namespace easyUTR.Models;

public partial class CustomerOrder
{
    public int OrderId { get; set; }

    public DateTime OrderTime { get; set; }

    public DateTime PaidTime { get; set; }

    public string CustomerId { get; set; }

    public int StoreId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<ItemsInOrder> ItemsInOrders { get; set; } = new List<ItemsInOrder>();

    public virtual Store Store { get; set; } = null!;
}
