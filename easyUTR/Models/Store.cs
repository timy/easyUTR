using System;
using System.Collections.Generic;

namespace easyUTR.Models;

public partial class Store
{
    public int StoreId { get; set; }

    public string StoreName { get; set; } = null!;

    public string StoreDescription { get; set; } = null!;

    public string? StoreImage { get; set; }

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<ItemsInStore> ItemsInStores { get; set; } = new List<ItemsInStore>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
