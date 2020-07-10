using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using COMP2084___A1.Models;
using Microsoft.AspNetCore.Authorization;

namespace COMP2084___A1.Controllers
{
    
    public class UserSelectionsController : Controller
    {
        private readonly COMP2084_A1_V3Context _context;

        public UserSelectionsController(COMP2084_A1_V3Context context)
        {
            _context = context;
        }
        [AllowAnonymous]
        // GET: UserSelections
        public async Task<IActionResult> Index()
        {
            var cOMP2084_A1_V3Context = _context.UserSelection.Include(u => u.EcoscoreNavigation);
            
            return View(await cOMP2084_A1_V3Context.ToListAsync());
        }

        // GET: UserSelections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSelection = await _context.UserSelection
                .Include(u => u.EcoscoreNavigation)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userSelection == null)
            {
                return NotFound();
            }

            return View(userSelection);
        }

        // GET: UserSelections/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["EcoscoreId"] = new SelectList(_context.EcoScore.OrderBy(c => c.EcoscoreId), "EcoscoreId", "EcoscoreId");
            ViewData["objectName"] = new SelectList(_context.EcoScore, "objectName", "objectName"); ////
            return View();
        }

        // POST: UserSelections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FullName,ObjectName,Description,Reuse,Reduce,Recycle,Photo,Ecoscore,EcoscoreId")] UserSelection userSelection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userSelection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EcoscoreId"] = new SelectList(_context.EcoScore.OrderBy(c => c.EcoscoreId), "EcoscoreId", "EcoscoreId", userSelection.EcoscoreId);
            ViewData["objectName"] = new SelectList(_context.EcoScore, "objectName", "objectName", userSelection.ObjectName);
            return View(userSelection);
        }

        // GET: UserSelections/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSelection = await _context.UserSelection.FindAsync(id);
            if (userSelection == null)
            {
                return NotFound();
            }
            ViewData["EcoscoreId"] = new SelectList(_context.EcoScore.OrderBy(c => c.EcoscoreId), "EcoscoreId", "EcoscoreId", userSelection.EcoscoreId);
            ViewData["objectName"] = new SelectList(_context.EcoScore, "objectName", "objectName", userSelection.ObjectName);
            return View(userSelection);
        }

        // POST: UserSelections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,FullName,ObjectName,Description,Reuse,Reduce,Recycle,Photo,Ecoscore,EcoscoreId")] UserSelection userSelection)
        {
            if (id != userSelection.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userSelection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserSelectionExists(userSelection.UserId))
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
            ViewData["EcoscoreId"] = new SelectList(_context.EcoScore.OrderBy(c => c.EcoscoreId), "EcoscoreId", "EcoscoreId", userSelection.EcoscoreId);
            ViewData["objectName"] = new SelectList(_context.EcoScore, "objectName", "objectName", userSelection.ObjectName);
            return View(userSelection);
        }

        // GET: UserSelections/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSelection = await _context.UserSelection
                .Include(u => u.EcoscoreNavigation)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userSelection == null)
            {
                return NotFound();
            }

            return View(userSelection);
        }

        // POST: UserSelections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userSelection = await _context.UserSelection.FindAsync(id);
            _context.UserSelection.Remove(userSelection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserSelectionExists(int id)
        {
            return _context.UserSelection.Any(e => e.UserId == id);
        }
    }
}
