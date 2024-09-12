using easyUTR.Models;

namespace easyUTR.ViewModels.Items
{
    public class ItemDetailViewModel
    {
        public Item Item { get; set; }
        public List<Item> RelatedItems { get; set; }
    }
}
