namespace easyUTR.ViewModels.Stores
{
    public class StoreMapInfo
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; } = null!;
        public string StoreDescription { get; set; } = null!;
        public string StoreImage { get; set; } = null!;
        public int AddressId { get; set; }
        public string AddressLine { get; set; } = null!;
        public string Suburb { get; set; } = null!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
