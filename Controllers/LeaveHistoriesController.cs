using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IT_Norr.Data;
using IT_Norr.Models;

namespace IT_Norr.Controllers
{
    public class LeaveHistoriesController : Controller
    {
        private readonly ITnorrcontext _context;

        public LeaveHistoriesController(ITnorrcontext context)
        {
            _context = context;
        }

        // GET: LeaveHistories

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LeaveHistories
                .Include(l => l.Employee)
                .Include(l => l.Leave);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeaveHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveHistory = await _context.LeaveHistories
                .Include(l => l.Employee)
                .Include(l => l.Leave)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveHistory == null)
            {
                return NotFound();
            }

            return View(leaveHistory);
        }       
    }
}
