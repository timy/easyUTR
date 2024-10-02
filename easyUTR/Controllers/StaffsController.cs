using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using easyUTR.Data;
using easyUTR.Models;
using Microsoft.AspNetCore.Identity;
using easyUTR.Areas.Identity.Data;
using easyUTR.ViewModels.Staffs;

namespace easyUTR.Controllers
{
    public class StaffsController : Controller
    {
        private readonly EasyUtrContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StaffsController(EasyUtrContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Staffs
        public async Task<IActionResult> Index()
        {
            var query = _context.Staff.Include(s => s.Job).Include(s => s.Store);
            var staffs = await query.Select(i => new StaffInfo
            {
                StaffId = i.StaffId,
                Email = i.Email,
                FirstName = i.FirstName,
                LastName = i.LastName,
                StoreName = i.Store.StoreName,
                JobName = i.Job.JobName,
                JobLevel = i.JobLevel,
            }).ToListAsync();
            var vm = new StaffIndexViewModel { Staffs = staffs };
            return View(vm);
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .Include(s => s.Job)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Staffs/Create
        public async Task<IActionResult> Create()
        {
            var stores = await _context.Stores
                .OrderBy(i => i.StoreName)
                .Select(i => new
                {
                    i.StoreId,
                    i.StoreName,
                }).ToListAsync();
            var storeList = new SelectList(stores, nameof(Store.StoreId), nameof(Store.StoreName));

            var jobs = await _context.Jobs
                .OrderBy(i => i.JobName)
                .Select(i => new
                {
                    i.JobId,
                    i.JobName,
                }).ToListAsync();
            var jobList = new SelectList(jobs, nameof(Job.JobId), nameof(Job.JobName));

            var vm = new StaffCreateViewModel
            {
                StoreList = storeList,
                JobList = jobList,
            };

            return View(vm);
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StaffCreateViewModel vm)
        {
            ModelState.Remove("StoreList");
            ModelState.Remove("JobList");
            if (TryValidateModel(vm.SubmitDTO))
            {
                var user = new AppUser { UserName = vm.SubmitDTO.Email, Email = vm.SubmitDTO.Email };
                var result = await _userManager.CreateAsync(user, "DefaultPassword@123");
                if (result.Succeeded)
                {
                    // Add role
                    await _userManager.AddToRoleAsync(user, "Manager");
                    // Save to Staff of the current context
                    var staff = new Staff
                    {
                        StaffId = user.Id,
                        Email = vm.SubmitDTO.Email,
                        FirstName = vm.SubmitDTO.FirstName,
                        LastName = vm.SubmitDTO.LastName,
                        StoreId = vm.SubmitDTO.StoreId,
                        JobId = vm.SubmitDTO.JobId,
                    };
                    _context.Add(staff);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var stores = await _context.Stores
                .OrderBy(i => i.StoreName)
                .Select(i => new
                {
                    i.StoreId,
                    i.StoreName,
                }).ToListAsync();
            vm.StoreList = new SelectList(stores, nameof(Store.StoreId), nameof(Store.StoreName));

            var jobs = await _context.Jobs
                .OrderBy(i => i.JobName)
                .Select(i => new
                {
                    i.JobId,
                    i.JobName,
                }).ToListAsync();
            vm.JobList = new SelectList(jobs, nameof(Job.JobId), nameof(Job.JobName));

            return View(vm);
        }

        // GET: Staffs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", staff.JobId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreId", staff.StoreId);
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StaffId,Email,FirstName,LastName,StoreId,JobId,JobLevel")] Staff staff)
        {
            if (id != staff.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.StaffId))
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
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", staff.JobId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreId", staff.StoreId);
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .Include(s => s.Job)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff != null)
            {
                _context.Staff.Remove(staff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(string id)
        {
            return _context.Staff.Any(e => e.StaffId == id);
        }
    }
}
