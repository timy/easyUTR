using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using easyUTR.Data;
using easyUTR.Models;
using easyUTR.ViewModels;
using easyUTR.DetailModel;
using Humanizer;

namespace easyUTR.Controllers
{
    public class StoresController : Controller
    {
        private readonly EasyUtrContext _context;

        public StoresController(EasyUtrContext context)
        {
            _context = context;
        }

        // GET: Stores
        public async Task<IActionResult> Index()
        {
            var easyUtrContext = _context.Stores.Include(s => s.Address);
            return View(await easyUtrContext.ToListAsync());
        }

        // GET: Stores/Details/5
        public async Task<IActionResult> Details(int storeId, StoreItemViewModel vm)
        {
            // Get store information
            var store = await _context.Stores
                .Include(s => s.Address)
                .FirstOrDefaultAsync(m => m.StoreId == storeId);
            if (store == null)
            {
                return NotFound();
            }

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

            // Prepare supplier list for dropdown
            var suppliers = await _context.Suppliers
                .OrderBy(i => i.SupplierName)
                .Select(i => new {
                    i.SupplierId,
                    i.SupplierName
                })
                .ToListAsync();

            var query = from itemsInStore in _context.ItemsInStores
                    where itemsInStore.StoreId == storeId
                    join item in _context.Items
                    on itemsInStore.ItemId equals item.ItemId
                    join category in _context.ItemCategories
                    on item.CategoryId equals category.CategoryId
                    join supplier in _context.Suppliers
                    on item.SupplierId equals supplier.SupplierId
                    select new StoreItemDetailModel {
                        Detail = new ItemDetailModel {
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
                        Price = itemsInStore.Price,
                        NumberInStock = itemsInStore.NumberInStock,
                    };

            // Filter by category
            if (vm.CategoryID.HasValue)
            {
                query = from record in query
                    where record.Detail.ParentCategoryId == vm.CategoryID || record.Detail.CategoryId == vm.CategoryID
                    select record;
            }

            // Filter by supplier
            if (vm.SupplierID.HasValue)
            {
                query = from record in query
                    where record.Detail.SupplierId == vm.SupplierID
                    select record;
            }

            // Filter by item name
            if (!string.IsNullOrWhiteSpace(vm.SearchText))
            {
                query = from record in query
                    where record.Detail.ItemName.Contains(vm.SearchText)
                    select record;
            }

            // Group items by parent category
            var groupedItems = await query
                .GroupBy(i => i.Detail.ParentCategoryId ?? i.Detail.CategoryId)
                .ToDictionaryAsync(g => g.Key, g => g.ToList());

            vm.ParentCategories = parentCategories;
            vm.Store.StoreId = storeId;
            vm.Store.StoreName = store.StoreName;
            vm.Store.StoreAddress = $"{store.Address.AddressLine}, {store.Address.Suburb}";
            vm.Store.StoreDescription = store.StoreDescription;
            vm.Store.StoreImage = store.StoreImage ?? string.Empty;
            vm.Store.Items = groupedItems;
            vm.CategoryList = new SelectList(categories,
                nameof(ItemCategory.CategoryId),
                nameof(ItemCategory.CategoryName));
            vm.SupplierList = new SelectList(suppliers,
                nameof(Supplier.SupplierId),
                nameof(Supplier.SupplierName));

            return View(vm);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId");
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoreId,StoreName,StoreDescription,StoreImage,AddressId")] Store store)
        {
            if (ModelState.IsValid)
            {
                _context.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", store.AddressId);
            return View(store);
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", store.AddressId);
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StoreId,StoreName,StoreDescription,StoreImage,AddressId")] Store store)
        {
            if (id != store.StoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(store);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.StoreId))
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", store.AddressId);
            return View(store);
        }

        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .Include(s => s.Address)
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                _context.Stores.Remove(store);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(int id)
        {
            return _context.Stores.Any(e => e.StoreId == id);
        }




        // GET: Stores/EditStoreItem?storeId=5&itemId=1
        public async Task<IActionResult> EditStoreItem(int storeId, int itemId)
        {
            var queryItem = from item in _context.Items
                            where item.ItemId == itemId
                            select item;

            var query = from itemsInStore in _context.ItemsInStores
                        where itemsInStore.StoreId == storeId
                        join item in queryItem
                        on itemsInStore.ItemId equals item.ItemId
                        join category in _context.ItemCategories
                        on item.CategoryId equals category.CategoryId
                        join supplier in _context.Suppliers
                        on item.SupplierId equals supplier.SupplierId
                        select new StoreItemDetailModel
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
                            Price = itemsInStore.Price,
                            NumberInStock = itemsInStore.NumberInStock,
                        };

            var storeItem = await query.FirstOrDefaultAsync();
            var vm = new EditStoreItemViewModel
            {
                StoreId = storeId,
                SupplierList = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", storeItem.Detail.SupplierId),
                CategoryList = new SelectList(_context.ItemCategories, "CategoryId", "CategoryName", storeItem.Detail.CategoryId),
                Item = storeItem
            };
            return View(vm);
        }

        // POST: Stores/EditStoreItem?storeId=5&itemId=1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStoreItem(int storeId, int itemId, decimal price, int numberInStock)
        {
            var itemInStore = await _context.ItemsInStores
                .Where(i => i.StoreId == storeId && i.ItemId == itemId)
                .FirstOrDefaultAsync();

            if (itemInStore == null)
            {
                return NotFound();
            }

            itemInStore.Price = price;
            itemInStore.NumberInStock = numberInStock;

            try
            {
                _context.Update(itemInStore);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Details), new {storeId = storeId});
        }
    }
}
