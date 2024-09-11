using easyUTR.DetailModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace easyUTR.ViewModels
{
    public class EditStoreItemViewModel
    {
        public SelectList? SupplierList { get; set; }
        public SelectList? CategoryList { get; set; }
        public StoreItemDetailModel Item { get; set; }
    }
}
