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
    public class staffsController : Controller
    {
        private readonly ecomereceContext _context;
        private readonly IWebHostEnvironment _img;

        public staffsController(ecomereceContext context, IWebHostEnvironment img)
        {
            _context = context;
            _img = img;
        }

        // GET: staffs
        public async Task<IActionResult> Index()
        {
              return View(await _context.staff.ToListAsync());
        }

        // GET: staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.staff == null)
            {
                return NotFound();
            }

            var staff = await _context.staff.Where(m => m.StaffId == id).FirstOrDefaultAsync();
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: staffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,StaffName,StaffPhone,StaffEmail,City,StaffAddress,StaffDob,SystemUserId,Role,Status,MetaData,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,StaffImage")] staff staff, 
            IFormFile PP)
        {
            string FindExt = Path.GetExtension(PP.FileName);
            string GiveName = Guid.NewGuid().ToString();
            string Basepath = "/data/img/profpic/";
            string FinalPath = Basepath + GiveName + FindExt;
            using (FileStream StImg = new FileStream(_img.WebRootPath + FinalPath, FileMode.Create))
            {
                PP.CopyTo(StImg);
            }
            

            if (ModelState.IsValid)
            {
                staff.StaffImage = FinalPath;
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // GET: staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.staff == null)
            {
                return NotFound();
            }

            var staff = await _context.staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,StaffName,StaffPhone,StaffEmail,City,StaffAddress,StaffDob,SystemUserId,Role,Status,MetaData,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,StaffImage")] staff staff)
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
                    if (!staffExists(staff.StaffId))
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
            return View(staff);
        }

        // GET: staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.staff == null)
            {
                return NotFound();
            }

            var staff = await _context.staff
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.staff == null)
            {
                return Problem("Entity set 'ecomereceContext.staff'  is null.");
            }
            var staff = await _context.staff.FindAsync(id);
            if (staff != null)
            {
                _context.staff.Remove(staff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool staffExists(int id)
        {
          return _context.staff.Any(e => e.StaffId == id);
        }
    }
}
