using System;
using System.Collections.Generic;

namespace easyUTR.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string AddressLine { get; set; } = null!;

    public string Suburb { get; set; } = null!;

    public string Postcode { get; set; } = null!;

    public string Region { get; set; } = null!;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
