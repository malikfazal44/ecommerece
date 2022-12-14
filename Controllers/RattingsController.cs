using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ecommerece.Models;

namespace ecommerece.Controllers
{
    public class RattingsController : Controller
    {
        private readonly ecomereceContext _context;

        public RattingsController(ecomereceContext context)
        {
            _context = context;
        }

        // GET: Rattings
        public async Task<IActionResult> Index()
        {
              return View(await _context.Rattings.ToListAsync());
        }

        // GET: Rattings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rattings == null)
            {
                return NotFound();
            }

            var ratting = await _context.Rattings
                .FirstOrDefaultAsync(m => m.RattingId == id);
            if (ratting == null)
            {
                return NotFound();
            }

            return View(ratting);
        }

        // GET: Rattings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rattings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RattingId,CustId,ProductId,Rate,Remarks,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Ratting ratting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ratting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ratting);
        }

        // GET: Rattings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rattings == null)
            {
                return NotFound();
            }

            var ratting = await _context.Rattings.FindAsync(id);
            if (ratting == null)
            {
                return NotFound();
            }
            return View(ratting);
        }

        // POST: Rattings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RattingId,CustId,ProductId,Rate,Remarks,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Ratting ratting)
        {
            if (id != ratting.RattingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ratting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RattingExists(ratting.RattingId))
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
            return View(ratting);
        }

        // GET: Rattings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rattings == null)
            {
                return NotFound();
            }

            var ratting = await _context.Rattings
                .FirstOrDefaultAsync(m => m.RattingId == id);
            if (ratting == null)
            {
                return NotFound();
            }

            return View(ratting);
        }

        // POST: Rattings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rattings == null)
            {
                return Problem("Entity set 'ecomereceContext.Rattings'  is null.");
            }
            var ratting = await _context.Rattings.FindAsync(id);
            if (ratting != null)
            {
                _context.Rattings.Remove(ratting);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RattingExists(int id)
        {
          return _context.Rattings.Any(e => e.RattingId == id);
        }
    }
}
