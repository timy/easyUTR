using Microsoft.Identity.Client;

namespace easyUTR.ViewModels.CustomerOrders
{
    public class ItemInOrderDescription
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } =string.Empty;
        public int OrderId { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal TotalItemCost { get; set; }
    }
}
