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
using Microsoft.Identity.Client;

namespace easyUTR.Controllers
{
    public class ItemsController : Controller
    {
        private readonly EasyUtrContext _context;

        public ItemsController(EasyUtrContext context)
        {
            _context = context;
        }

        // GET: Items
        //public async Task<IActionResult> Index(ItemSearchViewModel vm)
        //{
        //    // Query categories
        //    var categories = _context.ItemCategories
        //        .Where(i => !i.ParentCategoryId.HasValue)
        //        .OrderBy(i => i.CategoryName)
        //        .Select(i => new
        //        {
        //            i.CategoryId,
        //            i.CategoryName
        //        })
        //        .ToList();
        //    vm.CategoryList = new SelectList(categories,
        //        nameof(ItemCategory.CategoryId),
        //        nameof(ItemCategory.CategoryName));

        //    // Retrieve all items
        //    var dbContext = _context.Items
        //        .Include(i => i.Category)
        //        .Include(i => i.Supplier)
        //        .AsQueryable();

        //    // Filter by category
        //    if (vm.CategoryID != null)
        //    {
        //        dbContext = dbContext
        //            .Where(i => i.Category.ParentCategoryId == vm.CategoryID || i.CategoryId == vm.CategoryID);
        //    }

        //    // Filter by item name
        //    if (!string.IsNullOrWhiteSpace(vm.SearchText))
        //    {
        //        dbContext = dbContext.Where(i => i.ItemName.Contains(vm.SearchText));
        //    }

        //    // Sort by item name
        //    dbContext = dbContext.OrderBy(i => i.ItemName);

        //    vm.ItemList = await dbContext.ToListAsync();

        //    return View(vm);
        //}


        public async Task<IActionResult> Index(ItemSearchViewModel vm)
        {
            // Query parent categories
            var parentCategories = await _context.ItemCategories
                .Where(c => !c.ParentCategoryId.HasValue)
                .OrderBy(c => c.CategoryName)
                .ToListAsync();

            // Prepare category list for dropdown
            var categories = _context.ItemCategories
                .OrderBy(i => i.CategoryName)
                .Select(i => new
                {
                    i.CategoryId,
                    i.CategoryName
                })
                .ToList();
            vm.CategoryList = new SelectList(categories,
                nameof(ItemCategory.CategoryId),
                nameof(ItemCategory.CategoryName));

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
                CategoryList = vm.CategoryList
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

            var item = await _context.Items
                .Include(i => i.Category)
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            //return View(item);
            // Fetch related items from the same parent category
            var relatedItems = await _context.Items
                .Include(i => i.Category)
                .Where(i => i.Category.ParentCategoryId == item.Category.ParentCategoryId
                            && i.ItemId != item.ItemId)
                .Take(3)  // Limit to 3 related items
                .ToListAsync();

            var viewModel = new ItemDetailViewModel
            {
                Item = item,
                RelatedItems = relatedItems
            };

            return View(viewModel);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,ItemDescription,ItemImage,CategoryId,SupplierId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", item.SupplierId);
            return View(item);
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
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", item.SupplierId);
            return View(item);
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
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", item.SupplierId);
            return View(item);
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
    }
}
