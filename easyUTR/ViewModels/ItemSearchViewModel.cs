using Microsoft.AspNetCore.Mvc.Rendering;
using easyUTR.Models;

namespace easyUTR.ViewModels
{
    public class ItemSearchViewModel
    {
        public string? SearchText { get; set; }
        public int? CategoryID { get; set; }
        public SelectList? CategoryList { get; set; }
        public List<Item>? ItemList { get; set; }
    }

}


