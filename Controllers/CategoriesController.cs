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
    public class CategoriesController : Controller
    {
        private readonly ecomereceContext _context;
        private readonly IWebHostEnvironment _img;

        public CategoriesController(ecomereceContext context, IWebHostEnvironment img)
        {
            _context = context;
            _img = img;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
              return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatId,CatName,CatDesc,CatStatus,MetaData,SeoData,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,CatImage")] 
        Category category, IFormFile Catimg)
        {

            string FindExt = Path.GetExtension(Catimg.FileName);
            string GiveName = Guid.NewGuid().ToString();
            string FinalPath = "/data/img/catpic/" + GiveName + FindExt;
            using (FileStream CatFS = new FileStream(_img.WebRootPath + FinalPath, FileMode.Create))
            {
                await Catimg.CopyToAsync(CatFS);
            }

            if (ModelState.IsValid)
            {
                category.CatImage = FinalPath;
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatId,CatName,CatDesc,CatStatus,MetaData,SeoData,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,CatImage")] 
        Category category,IFormFile Catimg)
        {
            if (id != category.CatId)
            {
                return NotFound();
            }

            string FindExt = Path.GetExtension(Catimg.FileName);
            string GiveName = Guid.NewGuid().ToString();
            string FinalPath = "/data/img/catpic/" + GiveName + FindExt; 
            using (FileStream CatFS = new FileStream(_img.WebRootPath + FinalPath , FileMode.Create))
            {
                await Catimg.CopyToAsync(CatFS);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    category.CatImage = FinalPath;
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CatId))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ecomereceContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {

                var FilePath = category.CatImage;
                string FinalPath = _img.WebRootPath + FilePath;
                FileInfo File = new FileInfo(FinalPath);
                if (File.Exists)
                {
                    File.Delete();
                }

                _context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return _context.Categories.Any(e => e.CatId == id);
        }
    }
}
