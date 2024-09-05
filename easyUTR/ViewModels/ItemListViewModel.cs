using easyUTR.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace easyUTR.ViewModels
{
    public class ItemListViewModel
    {
        public List<ItemCategory> ParentCategories { get; set; }
        public Dictionary<int, List<Item>> GroupedItems { get; set; }
        public string? SearchText { get; set; }
        public int? CategoryID { get; set; }
        public SelectList CategoryList { get; set; }
    }
}
