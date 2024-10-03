using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using easyUTR.Data;
using easyUTR.Models;
using easyUTR.ViewModels.CustomerOrders;
using Microsoft.AspNetCore.Identity;
using Humanizer;

namespace easyUTR.Controllers
{
    public class CustomerOrdersController : Controller
    {
        private readonly EasyUtrContext _context;

        public CustomerOrdersController(EasyUtrContext context)
        {
            _context = context;
        }

        // GET: CustomerOrders
        public async Task<IActionResult> Index()
        {
            var query = _context.CustomerOrders
                .Include(c => c.Customer)
                .Join(_context.ItemsInOrders, c => c.OrderId, i => i.OrderId, (c, i) => new
                {
                    OrderId = c.OrderId,
                    OrderTime = c.OrderTime,
                    CustomerId = c.Customer.CustomerId,
                    CustomerName = $"{c.Customer.FirstName} {c.Customer.LastName}",
                    ItemId = i.ItemId,
                    StoreId = i.StoreId,
                    Quantity = i.NumberOf,
                    Cost = i.TotalItemCost,
                })
                .GroupBy(i => i.OrderId)
                .Select(g => new CustomerOrderInfo
                {
                    OrderId = g.Key,
                    OrderTime = g.Select(i => i.OrderTime).First(),
                    CustomerId = g.Select(i => i.CustomerId).First(),
                    CustomerName = g.Select(i => i.CustomerName).First(),
                    ItemsInOrder = g.Select(i => new ItemInOrderDescription
                    {
                        ItemId = i.ItemId,
                        ItemName = _context.Items.Where(itm => itm.ItemId == i.ItemId).Select(itm => itm.ItemName).First(),
                        OrderId = i.OrderId,
                        StoreId = i.StoreId,
                        StoreName = _context.Stores.Where(sto => sto.StoreId == i.StoreId).Select(sto => sto.StoreName).First(),
                        Quantity = i.Quantity,
                        TotalItemCost = i.Cost
                    }).ToList(),
                    TotalPrice = g.Sum(i => i.Cost),
                });

            var orders = await query.ToListAsync();
            var vm = new CustomerOrderIndexViewModel {
                Orders = orders
            };
            return View(vm);
        }

        // GET: CustomerOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerOrder = await _context.CustomerOrders
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (customerOrder == null)
            {
                return NotFound();
            }

            return View(customerOrder);
        }

        private bool CustomerOrderExists(int id)
        {
            return _context.CustomerOrders.Any(e => e.OrderId == id);
        }
    }
}
