using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IT_Norr.Data;
using IT_Norr.Models;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace IT_Norr.Controllers
{
    public class LeavesController : Controller
    {
        private readonly ITnorrcontext _context;

        public LeavesController(ITnorrcontext context)
        {
            _context = context;
        }

        // GET: Leaves
        public async Task<IActionResult> Index()
        {
            var iTnorrcontext = _context.Leaves.Include(l => l.Employee);
            return View(await iTnorrcontext.ToListAsync());
        }

        // GET: Leaves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leave = await _context.Leaves
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leave == null)
            {
                return NotFound();
            }

            return View(leave);
        }

        // GET: Leaves/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name");
            return View();
        }

        // POST: Leaves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate,Type,RequestStatus,Time,EmployeeId")] Leave leave)
        {
            if (leave.EndDate < leave.StartDate)
            {
                ModelState.AddModelError("EndDate", "End date cannot be earlier than start date.");
            }
            if (ModelState.IsValid)
            {
                _context.Add(leave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", leave.EmployeeId);
            return View(leave);
        }

        // GET: Leaves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leave = await _context.Leaves.FindAsync(id);
            if (leave == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", leave.EmployeeId);
            return View(leave);
        }

        // POST: Leaves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,RequestStatus,Type,Time,EmployeeId")] Leave leave)
        {
            if (leave.EndDate < leave.StartDate)
            {
                ModelState.AddModelError("EndDate", "End date cannot be earlier than start date.");
            }
            if (id != leave.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leave);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveExists(leave.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", leave.EmployeeId);
            return View(leave);
        }

        // GET: Leaves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leave = await _context.Leaves
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leave == null)
            {
                return NotFound();
            }

            return View(leave);
        }

        // POST: Leaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leave = await _context.Leaves.FindAsync(id);
            if (leave != null)
            {
                _context.Leaves.Remove(leave);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveExists(int id)
        {
            return _context.Leaves.Any(e => e.Id == id);
        }

        // GET: Leave/FilterByMonth
        public IActionResult FilterByMonth()
        {
            return View();
        }

        // POST: Leave/FilterByMonth
        [HttpPost]
        public IActionResult FilterByMonth(int month)
        {
            var leaves = _context.Leaves
                                 .Include(l => l.Employee)
                                 .Where(l => l.Time.Month == month)
                                 .ToList();

            return View("ResultByMonth", leaves); // Assuming you have a view named "FilteredLeaves"
        }

        public async Task<IActionResult> LeaveHistoryList()
        {
            var applicationDbContext = _context.Leaves
                .Include(l => l.Employee);
              
            return View(await applicationDbContext.ToListAsync());
        }

        // GET method to render the filter form
        [HttpGet]
        public IActionResult FilterByEmployee()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name");
            return View();
        }

        // POST method to handle filtering logic
        [HttpPost]
        public async Task<IActionResult> FilterByEmployee(int id)
        {
            var leaves = await _context.Leaves
                .Include(l => l.Employee)
                .Where(l => l.EmployeeId == id)
                .ToListAsync();

            return View("ResultByEmployee", leaves);
        }
        public async Task<IActionResult> LeaveHistoryDetails()
        {
            var iTnorrcontext = _context.Leaves.Include(l => l.Employee);
            return View(await iTnorrcontext.ToListAsync()); 
        }

    }

}

