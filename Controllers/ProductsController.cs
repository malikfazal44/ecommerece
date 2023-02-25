using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ecommerece.Models;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.AspNetCore.Identity;

namespace ecommerece.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ecomereceContext _context;
        private readonly IWebHostEnvironment _img;

        public ProductsController(ecomereceContext context,IWebHostEnvironment img)
        {
            _context = context;
            _img = img;
        }

        // GET: Products
        public IActionResult Index(int cat_id)
        {
            if (HttpContext.Session.GetInt32("UserId")!=null)
            {
                ViewBag.AllProducts = _context.Products.Where(p => p.SellerId == HttpContext.Session.GetInt32("UserId")).ToList().Count();
                ViewBag.ActiveProducts = _context.Products.Where(p => p.SellerId == HttpContext.Session.GetInt32("UserId") && p.Status==1).ToList().Count();
                ViewBag.InActiveProducts = _context.Products.Where(p => p.SellerId == HttpContext.Session.GetInt32("UserId") && p.Status == 0).ToList().Count();

            }
            ViewBag.List = _context.Products.ToList().Count();
            // following viewbag used in product's index
            ViewBag.Product_cards = _context.Products.ToList();
            if (cat_id != null && cat_id!=0)
            {
                return View(_context.Products.Where(cat => cat.CatId == cat_id));
            }
            else
            {
                return View(_context.Products.ToList());
            }

            
            
        }

        

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.CatList = _context.Categories.ToList();
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (HttpContext.Session.GetString("type") == "4")
            {
                return RedirectToAction("Index", "Home");
            }

            // following viewbag used in product's create
            ViewBag.CatList = _context.Categories.ToList();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,SellerId,DeliveryDays,CatId,Quantity,ShortDecsc,LongDesc,ProductPrice,DeliveryCharges,Status,MetaData,SeoData,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,ProductImg")] Product product,
            IList<IFormFile> prodImg)
        {
            
            string MultipleImages = "";
            foreach (var images in prodImg)
            {
                
                string FinalPath = "/data/img/products/" + Guid.NewGuid().ToString() + Path.GetExtension(images.FileName);
                using (FileStream proImg = new FileStream(_img.WebRootPath + FinalPath, FileMode.Create))
                {
                    images.CopyTo(proImg);
                }
                MultipleImages = MultipleImages+ "," + FinalPath;

            }
            

            if (ModelState.IsValid)
            {
                if (MultipleImages.StartsWith(","))
                {
                    MultipleImages = MultipleImages.Remove(0, 1);
                }
                product.SellerId = HttpContext.Session.GetInt32("UserId") ?? 0;
                product.ProductImg = MultipleImages;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // following viewbag used in product's edit
            ViewBag.CatList = _context.Categories.ToList();
            // authorization restricted for public and customer
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (HttpContext.Session.GetString("type") == "4")
            {
                return RedirectToAction("Index", "Home");
            }


            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,SellerId,DeliveryDays,CatId,Quantity,ShortDecsc,LongDesc,ProductPrice,DeliveryCharges,Status,MetaData,SeoData,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,ProductImg")] 
        Product product, IList<IFormFile>? prodImg)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }
            if (prodImg.Count >0 ) {
                string MultiImages = "";
                foreach (var file in prodImg)
                {


                    string FinalPath = "/data/img/products/" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    using (FileStream ProdFS = new FileStream(_img.WebRootPath, FileMode.Create))
                    {

                        file.CopyTo(ProdFS);
                    }
                    MultiImages += "," + FinalPath;
                    if (MultiImages.StartsWith(","))
                    {
                        MultiImages = MultiImages.Remove(0, 1);
                    }
                    product.ProductImg = MultiImages;
                }
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // authorization restricted for public and customer
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (HttpContext.Session.GetString("type") == "4")
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ecomereceContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                var FilePath = product.ProductImg;
                string FinalPath = _img.WebRootPath + FilePath;
                FileInfo File = new FileInfo(FinalPath);
                if (File.Exists)
                {
                    File.Delete();
                }

                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Delete using ajax
        [HttpPost]
        public bool DeleteBtn(int id)
        {
            


            try {
                var Product_id = _context.Products.Find(id);
                _context.Products.Remove(Product_id);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
            
        }
        public int QuantityDetail(int id)
        {
            var ProductId = _context.Products.Find(id);
            ProductId.Quantity = id;
            return id;
        }

        private bool ProductExists(int id)
        {
          return _context.Products.Any(e => e.ProductId == id);
        }

        //load function of ajax for not showing in DOM
        public string GetRI(int id, int cid)
        {

            var CtgryLoad = _context.Products.Where(prods => prods.CatId==cid).Take(5) ;

            string FinalPics = "";
            foreach(Product P in CtgryLoad)
            {
                var separate = P.ProductImg.Split(',');
                FinalPics += "<div class='col-4 m-2'><div class='card' style=width: 18rem;'>" +
                    "  <img class='card-img-top' src='" + separate[0]  +"' style = 'max-height: 300px' >  " +
                    "<div class='card-body'>    " +
                    "<h5 class='card-title'>"+ P.ProductName +"</h5>    " +
                    "<p class='card-text'>"+ P.LongDesc +"</p>    " +
                    "<a href='#' class='btn btn-primary'>Check Detail</a>  </div></div></div>";
            }

            return FinalPics;
            //return "<div class='col-4'>        " +
            //    "<div class='img img-thumbnail'>            " +
            //    "<img src='/data/img/products/f63588de-285d-4e84-b3b0-505067b78559.jpeg' style='max-height: 100px;'/>        " +
            //    "</div>    </div>    " +
            //    "<div class='col-4'>        " +
            //    "<div class='img img-thumbnail'>            " +
            //    "<img src='/data/img/products/e63e933a-4223-47f5-9988-32632f1fe8b3.jpeg' style='max-height: 100px;' />        </div>    </div>    " +
            //    "<div class='col-4'>        " +
            //    "<div class='img img-thumbnail'>            " +
            //    "<img src='/data/img/products/d91c5430-61f0-4f27-8eb5-7025b3bdfd36.jpeg' style='max-height: 100px;' />        </div>    </div>";
         //   return "<h2 class = 'alert alert-danger'>This is action get ri from product controller</h2>";
        }
        public int? NumOfViews (int id)
        {
            var ViewCounter = _context.Products.Find(id).NumOfViews;
            return ViewCounter;
        }
        // list counter for showing in index
        

        public void GetCounter(int id)
        {
            var Counter = _context.Products.Find(id);
            Counter.NumOfViews = Counter.NumOfViews + 1;
            _context.Update(Counter);
            _context.SaveChanges();
        }
    }
}
