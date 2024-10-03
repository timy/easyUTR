using easyUTR.Models;

namespace easyUTR.ViewModels.CustomerOrders
{
    public class CustomerOrderInfo
    {
        public int OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public string CustomerId { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public List<ItemInOrderDescription> ItemsInOrder { get; set; } = new List<ItemInOrderDescription>();
        public decimal TotalPrice { get; set; }
    }
}
