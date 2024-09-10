namespace easyUTR.DetailModel
{
    public class StoreInventoryDetailModel
    {
        public int StoreID { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string StoreAddress {  get; set; } = string.Empty;
        public string StoreDescription { get; set; } = string.Empty;
        public string StoreImage { get; set; } = string.Empty;
        public Dictionary<int, List<StoreItemDetailModel>> Items { get; set; }
    }
}
