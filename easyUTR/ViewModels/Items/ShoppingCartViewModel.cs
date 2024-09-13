using easyUTR.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

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

        public string? paymentPublicKey { get; set; } = string.Empty;
    }
}