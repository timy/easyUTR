using Microsoft.AspNetCore.Mvc.Rendering;
using easyUTR.Models;

namespace easyUTR.ViewModels.Items
{
    public class ItemCreateViewModel
    {
        public SelectList? SupplierList { get; set; }
        public SelectList? CategoryList { get; set; }
        public Item? Item { get; set; }
    }
}
