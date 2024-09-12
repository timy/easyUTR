using easyUTR.DetailModel;
using easyUTR.Models;

namespace easyUTR.ViewModels.Items
{
    public class ItemDetailViewModel
    {
        public ItemDetailModel? Detail { get; set; }
        public List<ItemBriefModel>? RelatedItems { get; set; }
        public List<ItemStoreDetailModel>? ItemStores { get; set; }

    }
}
