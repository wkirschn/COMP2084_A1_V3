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
    
    public class EcoScoresController : Controller
    {
        private readonly COMP2084_A1_V3Context _context;

        public EcoScoresController(COMP2084_A1_V3Context context)
        {
            _context = context;
        }
        [AllowAnonymous]
        // GET: EcoScores
        public async Task<IActionResult> Index()
        {
            return View(await _context.EcoScore.ToListAsync());
        }

        // GET: EcoScores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ecoScore = await _context.EcoScore
                .FirstOrDefaultAsync(m => m.EcoscoreId == id);
            if (ecoScore == null)
            {
                return NotFound();
            }

            return View(ecoScore);
        }

        // GET: EcoScores/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: EcoScores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EcoscoreId,ObjectName,Description,Reuse,Reduce,Recycle,Photo,Ecoscore1")] EcoScore ecoScore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ecoScore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ecoScore);
        }

        // GET: EcoScores/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ecoScore = await _context.EcoScore.FindAsync(id);
            if (ecoScore == null)
            {
                return NotFound();
            }
            return View(ecoScore);
        }

        // POST: EcoScores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EcoscoreId,ObjectName,Description,Reuse,Reduce,Recycle,Photo,Ecoscore1")] EcoScore ecoScore)
        {
            if (id != ecoScore.EcoscoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ecoScore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EcoScoreExists(ecoScore.EcoscoreId))
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
            return View(ecoScore);
        }

        // GET: EcoScores/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ecoScore = await _context.EcoScore
                .FirstOrDefaultAsync(m => m.EcoscoreId == id);
            if (ecoScore == null)
            {
                return NotFound();
            }

            return View(ecoScore);
        }

        // POST: EcoScores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ecoScore = await _context.EcoScore.FindAsync(id);
            _context.EcoScore.Remove(ecoScore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EcoScoreExists(int id)
        {
            return _context.EcoScore.Any(e => e.EcoscoreId == id);
        }
    }
}
