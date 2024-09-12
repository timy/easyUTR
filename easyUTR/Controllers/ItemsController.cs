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

namespace easyUTR.Controllers
{
    public class ItemsController : Controller
    {
        private readonly EasyUtrContext _context;

        public ItemsController(EasyUtrContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(ItemSearchViewModel vm)
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

            // Retrieve all items
            var query = _context.Items
                .Include(i => i.Category)
                .Include(i => i.Supplier)
                .AsQueryable();

            // Filter by category
            if (vm.CategoryID != null)
            {
                query = query.Where(i => i.Category.ParentCategoryId == vm.CategoryID || i.CategoryId == vm.CategoryID);
            }

            // Filter by supplier
            if (vm.SupplierID != null)
            {
                query = query.Where(i => i.SupplierId == vm.SupplierID);
            }

            // Filter by item name
            if (!string.IsNullOrWhiteSpace(vm.SearchText))
            {
                query = query.Where(i => i.ItemName.Contains(vm.SearchText));
            }

            // Sort by item name
            query = query.OrderBy(i => i.ItemName);

            var items = await query.ToListAsync();

            // Group items by parent category
            var groupedItems = items
                .GroupBy(i => i.Category.ParentCategoryId ?? i.CategoryId)
                .ToDictionary(g => g.Key, g => g.ToList());

            var viewModel = new ItemListViewModel
            {
                ParentCategories = parentCategories,
                GroupedItems = groupedItems,
                SearchText = vm.SearchText,
                CategoryID = vm.CategoryID,
                CategoryList = vm.CategoryList,
                SupplierID = vm.SupplierID,
                SupplierList = vm.SupplierList
            };

            return View(viewModel);
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
        public IActionResult AddToCart(int ItemId, int ItemQuantity)
        {
            Item itemToAdd = _context.Items.Find(ItemId); // TODO: itemToAdd may be null here - redirect to out-of-stock message

            // Retrieve cart items
            List<ShoppingCartItem> cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? [];
            // Check if the item is already in the cart
            var existingCartItem = cartItems.FirstOrDefault(i => i.Item.ItemId == ItemId);
            if (existingCartItem != null)
            {
                // If already in the cart, only increase the quantity
                existingCartItem.Quantity += ItemQuantity;
            }
            else
            {
                // If not in the cart, create new cart item
                cartItems.Add(new ShoppingCartItem
                {
                    Item = itemToAdd,
                    Quantity = ItemQuantity
                });
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
                TotalQuantity = cartItems.Sum(i => i.Quantity)
                // TODO: Total price need to be evaluated according to which store the customer buys it
            };
            return View(cartViewModel);
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
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,ItemDescription,ItemImage,CategoryId,SupplierId")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
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

            ItemCreateViewModel vm = new ItemCreateViewModel
            {
                Item = item,
                SupplierList = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", item.SupplierId),
                CategoryList = new SelectList(_context.ItemCategories, "CategoryId", "CategoryName", item.CategoryId)
            };

            return View(vm);
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
    }
}
