using easyUTR.DetailModel;
using easyUTR.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace easyUTR.ViewModels
{
    public class StoreItemViewModel
    {
        public List<ItemCategory> ParentCategories { get; set; } = new List<ItemCategory>();
        // public Dictionary<int, List<StoreItemDetailModel>> GroupedItems { get; set; }
        public StoreInventoryDetailModel Store { get; set; } = new StoreInventoryDetailModel();
        public string? SearchText { get; set; }
        public int? CategoryId { get; set; }
        public SelectList CategoryList { get; set; }
        public int? SupplierId { get; set; }
        public SelectList SupplierList { get; set; }
    }
}
