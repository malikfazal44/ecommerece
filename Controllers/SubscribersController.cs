﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ecommerece.Models;

namespace ecommerece.Controllers
{
    public class SubscribersController : Controller
    {
        private readonly ecomereceContext _context;

        public SubscribersController(ecomereceContext context)
        {
            _context = context;
        }

        // GET: Subscribers
        public async Task<IActionResult> Index()
        {
              return View(await _context.Subscribers.ToListAsync());
        }

        // GET: Subscribers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Subscribers == null)
            {
                return NotFound();
            }

            var subscriber = await _context.Subscribers
                .FirstOrDefaultAsync(m => m.SubscriberId == id);
            if (subscriber == null)
            {
                return NotFound();
            }

            return View(subscriber);
        }

        // GET: Subscribers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subscribers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubscriberId,Status,SubscriberName,SubscriberEmail,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subscriber);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subscriber);
        }

        // GET: Subscribers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Subscribers == null)
            {
                return NotFound();
            }

            var subscriber = await _context.Subscribers.FindAsync(id);
            if (subscriber == null)
            {
                return NotFound();
            }
            return View(subscriber);
        }

        // POST: Subscribers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubscriberId,Status,SubscriberName,SubscriberEmail,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Subscriber subscriber)
        {
            if (id != subscriber.SubscriberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subscriber);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscriberExists(subscriber.SubscriberId))
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
            return View(subscriber);
        }

        // GET: Subscribers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Subscribers == null)
            {
                return NotFound();
            }

            var subscriber = await _context.Subscribers
                .FirstOrDefaultAsync(m => m.SubscriberId == id);
            if (subscriber == null)
            {
                return NotFound();
            }

            return View(subscriber);
        }

        // POST: Subscribers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Subscribers == null)
            {
                return Problem("Entity set 'ecomereceContext.Subscribers'  is null.");
            }
            var subscriber = await _context.Subscribers.FindAsync(id);
            if (subscriber != null)
            {
                _context.Subscribers.Remove(subscriber);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubscriberExists(int id)
        {
          return _context.Subscribers.Any(e => e.SubscriberId == id);
        }
    }
}
