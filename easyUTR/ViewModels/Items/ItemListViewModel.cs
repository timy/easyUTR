using easyUTR.DetailModel;
using easyUTR.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace easyUTR.ViewModels.Items
{
    public class ItemListViewModel
    {
        public List<ItemCategory> ParentCategories { get; set; }
        public Dictionary<int, List<ItemDetailModel>> GroupedItems { get; set; }
        public string? SearchText { get; set; }
        public int? CategoryId { get; set; }
        public SelectList CategoryList { get; set; }
        public int? SupplierId { get; set; }
        public SelectList SupplierList { get; set; }
    }
}
