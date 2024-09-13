namespace easyUTR.DetailModel
{
    public class ItemStatDetailModel
    {
        public ItemDetailModel Detail { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public int StoreNumber { get; set; } = 0;
    }
}
