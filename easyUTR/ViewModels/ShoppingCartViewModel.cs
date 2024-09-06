﻿using easyUTR.Models;

namespace easyUTR.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCartItem> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
    }
}
