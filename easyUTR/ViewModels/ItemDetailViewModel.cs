using easyUTR.Models;

namespace easyUTR.ViewModels
{
    public class ItemDetailViewModel
    {
        public Item Item { get; set; }
        public List<Item> RelatedItems { get; set; }
    }
}
