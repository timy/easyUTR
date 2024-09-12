using easyUTR.ViewModels.Items;

namespace easyUTR.ViewModels
{
    public class CheckoutViewModel
    {
        public ShoppingCartViewModel Cart { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiry { get; set; }
        public string CardCVV { get; set; }

        public CheckoutViewModel()
        {
            Cart = new ShoppingCartViewModel();
        }
    }
}
