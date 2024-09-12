using easyUTR.DetailModel;

namespace easyUTR.Models
{
    public class ShoppingCartItem
    {
        public Item? Item { get; set; }
        public ItemStoreDetailModel? ItemStore { get; set; }
        public int Quantity { get; set; }
    }
}