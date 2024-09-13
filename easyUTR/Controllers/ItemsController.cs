using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using easyUTR.Data;
using easyUTR.Models;
using easyUTR.ViewModels.Items;
using Microsoft.Identity.Client;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using easyUTR.DetailModel;
using easyUTR.ViewModels;

using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Stripe.Checkout;
using Microsoft.Extensions.Options;
using Stripe;

namespace easyUTR.Controllers
{
    public class ItemsController : Controller
    {
        private readonly EasyUtrContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private StripeSettings _stripeSettings;

        public ItemsController(EasyUtrContext context, IWebHostEnvironment hostEnvironment, IOptions<StripeSettings> stripeSettings)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _stripeSettings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }

        public async Task<IActionResult> Index(ItemListViewModel vm)
        {
            // Query parent categories
            var parentCategories = await _context.ItemCategories
                .Where(c => !c.ParentCategoryId.HasValue)
                .OrderBy(c => c.CategoryName)
                .ToListAsync();

            // Prepare category list for dropdown
            var categories = await _context.ItemCategories
                .OrderBy(i => i.CategoryName)
                .Select(i => new
                {
                    i.CategoryId,
                    i.CategoryName
                })
                .ToListAsync();
            vm.CategoryList = new SelectList(categories,
                nameof(ItemCategory.CategoryId),
                nameof(ItemCategory.CategoryName));


            // Prepare supplier list for dropdown
            var suppliers = await _context.Suppliers
                .OrderBy(i => i.SupplierName)
                .Select(i => new
                {
                    i.SupplierId,
                    i.SupplierName
                })
                .ToListAsync();
            vm.SupplierList = new SelectList(suppliers,
                nameof(Supplier.SupplierId),
                nameof(Supplier.SupplierName));

            // Prepare store list for dropdown
            var stores = await _context.Stores
                .OrderBy(i => i.StoreName)
                .Select(i => new
                {
                    i.StoreId,
                    i.StoreName,
                })
                .ToListAsync();
            vm.StoreList = new SelectList(stores,
                nameof(Store.StoreId),
                nameof(Store.StoreName));

            // Retrieve all items
            var query = _context.Items
                .Include(i => i.Category)
                .Include(i => i.Supplier)
                .AsQueryable();

            // Filter by category
            if (vm.CategoryId.HasValue)
            {
                query = query.Where(i => i.Category.ParentCategoryId == vm.CategoryId || i.CategoryId == vm.CategoryId);
            }

            // Filter by supplier
            if (vm.SupplierId.HasValue)
            {
                query = query.Where(i => i.SupplierId == vm.SupplierId);
            }

            // Filter by item name
            if (!string.IsNullOrWhiteSpace(vm.SearchText))
            {
                query = query.Where(i => i.ItemName.Contains(vm.SearchText));
            }

            // Sort by item name
            query = query.OrderBy(i => i.ItemName);


            var queryItemDetail = from item in query
                                  select new ItemDetailModel
                                  {
                                      ItemId = item.ItemId,
                                      ItemName = item.ItemName,
                                      ItemDescription = item.ItemDescription,
                                      ItemImage = item.ItemImage ?? string.Empty,
                                      CategoryId = item.Category.CategoryId,
                                      CategoryName = item.Category.CategoryName,
                                      ParentCategoryId = item.Category.ParentCategoryId,
                                      ParentCategoryName = item.Category.ParentCategoryId.HasValue ? (
                                               from c in _context.ItemCategories
                                               where c.CategoryId == item.Category.ParentCategoryId
                                               select c.CategoryName).ToString() : null,
                                      SupplierId = item.Supplier.SupplierId,
                                      SupplierName = item.Supplier.SupplierName,
                                      SupplierDescription = item.Supplier.SupplierDescription,
                                      SupplierUrl = item.Supplier.SupplierUrl
                                  };


            var queryItemStat = from itemDetail in queryItemDetail
                                join itemInStore in _context.ItemsInStores
                                on itemDetail.ItemId equals itemInStore.ItemId into g
                                select new ItemStatDetailModel
                                {
                                    Detail = itemDetail,
                                    MinPrice = g.Min(i => i.Price),
                                    MaxPrice = g.Max(i => i.Price),
                                    StoreNumber = g.Count()
                                };

            var items = await queryItemStat.ToListAsync();

            // Group items by parent category
            var groupedItems = items
                .GroupBy(i => i.Detail.ParentCategoryId ?? i.Detail.CategoryId)
                .ToDictionary(g => g.Key, g => g.ToList());

            vm.ParentCategories = parentCategories;
            vm.GroupedItems = groupedItems;
            return View(vm);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queryItem = from item in _context.Items
                            where item.ItemId == id
                            join category in _context.ItemCategories
                            on item.CategoryId equals category.CategoryId
                            join supplier in _context.Suppliers
                            on item.SupplierId equals supplier.SupplierId
                            select new
                            {
                                Detail = new ItemDetailModel
                                {
                                    ItemId = item.ItemId,
                                    ItemName = item.ItemName,
                                    ItemDescription = item.ItemDescription,
                                    ItemImage = item.ItemImage ?? string.Empty,
                                    CategoryId = category.CategoryId,
                                    CategoryName = category.CategoryName,
                                    ParentCategoryId = category.ParentCategoryId,
                                    ParentCategoryName = category.ParentCategoryId.HasValue ? (
                                 from c in _context.ItemCategories
                                 where c.CategoryId == category.ParentCategoryId
                                 select c.CategoryName).ToString() : null,
                                    SupplierId = supplier.SupplierId,
                                    SupplierName = supplier.SupplierName,
                                    SupplierDescription = supplier.SupplierDescription,
                                    SupplierUrl = supplier.SupplierUrl
                                },
                                RelatedItems = (from i in _context.Items
                                                where i.Category.ParentCategoryId == category.ParentCategoryId
                                                select new ItemBriefModel
                                                {
                                                    ItemId = i.ItemId,
                                                    ItemName = i.ItemName,
                                                    ItemDescription = i.ItemDescription,
                                                    ItemImage = i.ItemImage
                                                }).Take(3).ToList(),
                            };

            var queryStoreInfo = from itemsInStore in _context.ItemsInStores
                                 join item in queryItem
                                 on itemsInStore.ItemId equals item.Detail.ItemId
                                 join store in _context.Stores
                                 on itemsInStore.StoreId equals store.StoreId
                                 join address in _context.Addresses
                                 on store.AddressId equals address.AddressId
                                 select new ItemStoreDetailModel
                                 {
                                     StoreId = store.StoreId,
                                     StoreName = store.StoreName,
                                     StoreAddress = $"{store.Address.AddressLine}, {store.Address.Suburb}",
                                     Price = itemsInStore.Price,
                                     NumberInStock = itemsInStore.NumberInStock,
                                 };

            var itemInfo = await queryItem.FirstOrDefaultAsync();
            var storeInfo = await queryStoreInfo.ToListAsync();
            var viewModel = new ItemDetailViewModel
            {
                Detail = itemInfo?.Detail,
                RelatedItems = itemInfo?.RelatedItems,
                ItemStores = storeInfo,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int storeId, int itemId, int itemQuantity)
        {
            Item? itemToAdd = _context.Items.Find(itemId); // TODO: itemToAdd may be null here
            // TODO: storeToAdd may be null here
            // TODO: check itemsInStore to see if the item is out of stock

            var queryItemStore = from itemInStore in _context.ItemsInStores
                                 where itemInStore.ItemId == itemId && itemInStore.StoreId == storeId
                                 join store in _context.Stores
                                 on itemInStore.StoreId equals store.StoreId
                                 join address in _context.Addresses
                                 on store.AddressId equals address.AddressId
                                 select new ItemStoreDetailModel
                                 {
                                     StoreId = itemInStore.StoreId,
                                     StoreName = store.StoreName,
                                     StoreAddress = $"{address.AddressLine}, {address.Suburb}",
                                     Price = itemInStore.Price,
                                     NumberInStock = itemInStore.NumberInStock,
                                 };

            var itemStore = await queryItemStore.FirstOrDefaultAsync();

            // Retrieve cart items
            List<ShoppingCartItem> cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? [];
            // Check if the item is already in the cart
            var existingCartItem = cartItems.FirstOrDefault(i => i.Item?.ItemId == itemId && i.ItemStore?.StoreId == storeId);
            if (existingCartItem != null)
            {
                // If already in the cart, only increase the quantity
                existingCartItem.Quantity += itemQuantity;
            }
            else
            {
                // If not in the cart, create new cart item
                cartItems.Add(new ShoppingCartItem
                {
                    Item = itemToAdd,
                    ItemStore = itemStore,
                    Quantity = itemQuantity,
                });
            }
            HttpContext.Session.Set("Cart", cartItems);

            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(int storeId, int itemId, int itemQuantity)
        {
            Item? itemToAdd = _context.Items.Find(itemId); // TODO: itemToAdd may be null here
            // TODO: storeToAdd may be null here
            // TODO: check itemsInStore to see if the item is out of stock

            // Retrieve cart items
            List<ShoppingCartItem> cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? [];
            // Check if the item is already in the cart
            var existingCartItem = cartItems.FirstOrDefault(i => i.Item?.ItemId == itemId && i.ItemStore?.StoreId == storeId);
            if (existingCartItem.Quantity > itemQuantity)
            {
                // If already in the cart, only increase the quantity
                existingCartItem.Quantity -= itemQuantity;
            }
            else
            {
                cartItems.Remove(existingCartItem);
            }
            HttpContext.Session.Set("Cart", cartItems);

            return RedirectToAction("ViewCart");
        }


        public IActionResult ViewCart()
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? [];
            var cartViewModel = new ShoppingCartViewModel
            {
                CartItems = cartItems,
                TotalPrice = cartItems.Sum(i => i.Quantity * i.ItemStore.Price),
                TotalQuantity = cartItems.Sum(i => i.Quantity),
                paymentPublicKey = _stripeSettings.PublicKey,
            };
            return View(cartViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCheckoutSession(ShoppingCartViewModel vm)
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? [];
            var options = new SessionCreateOptions
            {
                LineItems = cartItems.Select(cartItem => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions {
                        UnitAmount = Convert.ToInt32(cartItem.ItemStore.Price * 100),
                        Currency = "aud",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = cartItem.Item.ItemName,
                        },
                    },
                    Quantity = cartItem.Quantity,
                }).ToList(),
                Mode = "payment",
                SuccessUrl = Url.Action("PaySuccess", "Items", null, Request.Scheme),
                CancelUrl = Url.Action("PayFailed", "Items", null, Request.Scheme),
            };

            try
            {
                var service = new SessionService();
                Session session = await service.CreateAsync(options);
                return Json(new { id = session.Id });
            }
            catch (Stripe.StripeException e)
            {
                return BadRequest(new { error = e.Message });
            }

            //Response.Headers.Add("Location", session.Url);
            //return new StatusCodeResult(300);
        }


        public IActionResult PaySuccess()
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? [];
            // TODO: transaction record
            return View();
        }

        public IActionResult PayFailed()
        {
            return View();
        }



        // GET: Items/Create
        public IActionResult Create()
        {
            ItemCreateViewModel vm = new ItemCreateViewModel
            {
                Item = null,
                SupplierList = new SelectList(_context.Suppliers, "SupplierId", "SupplierName"),
                CategoryList = new SelectList(_context.ItemCategories, "CategoryId", "CategoryName")
            };
            return View(vm);
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemName,ItemDescription,ItemImage,CategoryId,SupplierId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ItemCreateViewModel vm = new ItemCreateViewModel
            {
                Item = item,
                SupplierList = new SelectList(_context.Suppliers, "SupplierId", "SupplierName"),
                CategoryList = new SelectList(_context.ItemCategories, "CategoryId", "CategoryName")
            };
            return View(vm);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ItemCreateViewModel vm = new ItemCreateViewModel
            {
                Item = item,
                SupplierList = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", item.SupplierId),
                CategoryList = new SelectList(_context.ItemCategories, "CategoryId", "CategoryName", item.CategoryId)
            };
            return View(vm);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemCreateViewModel model, IFormFile ImageFile)
        {
            if (id != model.Item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        // Handle file upload
                        var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images", "items");
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(fileStream);
                        }

                        model.Item.ItemImage = "/images/items/" + uniqueFileName;
                    }
                    else if (string.IsNullOrWhiteSpace(model.Item.ItemImage))
                    {
                        // If no file was uploaded and no URL was provided, keep the existing image
                        var existingItem = await _context.Items.AsNoTracking().FirstOrDefaultAsync(i => i.ItemId == id);
                        model.Item.ItemImage = existingItem.ItemImage;
                    }
                    // If a new URL was provided in model.Item.ItemImage, it will be used as is

                    _context.Update(model.Item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(model.Item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            model.CategoryList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.ItemCategories, "CategoryId", "CategoryName", model.Item.CategoryId);
            model.SupplierList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Suppliers, "SupplierId", "SupplierName", model.Item.SupplierId);
            return View(model);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }

        private ShoppingCartViewModel GetCart()
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();
            var cart = new ShoppingCartViewModel
            {
                CartItems = cartItems,
                TotalQuantity = cartItems.Sum(i => i.Quantity),

            };
            return cart;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = GetCart();
            if (cart.CartItems.Count == 0)
            {
                TempData["Message"] = "Your cart is empty. Please add items before proceeding to checkout.";
                return RedirectToAction("ViewCart");
            }

            var checkoutViewModel = new CheckoutViewModel
            {
                Cart = cart
            };

            return View(checkoutViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Here you would typically:
            // 1. Process the payment
            // 2. Create an order in your database
            // 3. Clear the cart

            // For now, we'll just simulate a successful payment
            var paymentResult = ProcessPayment(model);

            if (paymentResult)
            {
                // Clear the cart
                HttpContext.Session.Remove("Cart");

                // Redirect to a thank you page
                return RedirectToAction("ThankYou");
            }
            else
            {
                ModelState.AddModelError("", "Payment processing failed. Please try again.");
                return View(model);
            }
        }

        public IActionResult ThankYou()
        {
            return View();
        }

        private bool ProcessPayment(CheckoutViewModel model)
        {
            // Simulate payment processing
            // In a real application, you would integrate with a payment provider here
            return true; // Always return true for now
        }

        public async Task<IActionResult> FoodAndBeverage()
        {

            return View();
        }

        public async Task<IActionResult> Fuel()
        {


            return View();
        }
    }
}
