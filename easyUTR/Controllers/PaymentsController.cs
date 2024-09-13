using easyUTR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace easyUTR.Controllers
{
    public class PaymentsController : Controller
    {
        private StripeSettings _stripeSettings;

        public PaymentsController(IOptions<StripeSettings> stripeSettings) {
            _stripeSettings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCheckoutSession()
        {
            var options = new SessionCreateOptions {
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = 2000, // TODO: price
                            Currency = "aud",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "T-shirt", // TODO: itemName
                            },
                        },
                        Quantity = 1, // TODO
                    },
                },
                Mode = "payment",
                SuccessUrl = "",  // TODO
                CancelUrl = "",   // TODO
            };

            var service = new SessionService();
            Session session = service.Create(options);
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(300);
        }
    }
}
