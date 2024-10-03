using easyUTR.Models;  
using Microsoft.AspNetCore.Mvc.Rendering; 
using System.Collections.Generic; 

namespace easyUTR.ViewModels.OrderHistory
{
    public class OrderHistoryViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime PaidTime { get; set; }
        public List<OrderItemViewModel> Items { get; set; } 
        public int? StoreId { get; set; }
        public SelectList StoreList { get; set; }
        public int? SupplierId { get; set; }
        public SelectList SupplierList { get; set; }
    }

    public class OrderItemViewModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } 
        public int StoreId { get; set; }
        public string StoreName { get; set; } 
        public int NumberOf { get; set; }
        public decimal TotalItemCost { get; set; }
    }
}
