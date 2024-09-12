using easyUTR.Models;

namespace easyUTR.ViewModels.Items
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCartItem> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }

        public ShoppingCartViewModel()
        {
            CartItems = new List<ShoppingCartItem>();
        }
    }
}