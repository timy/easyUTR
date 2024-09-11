using easyUTR.DetailModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace easyUTR.ViewModels
{
    public class EditStoreItemViewModel
    {
        public int StoreId { get; set; }
        public SelectList? SupplierList { get; set; }
        public SelectList? CategoryList { get; set; }
        public StoreItemDetailModel Item { get; set; }
    }
}
