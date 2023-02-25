using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ecommerece.Models;
using Newtonsoft.Json;
using ecommerece.Models.ViewModels;
using Microsoft.CodeAnalysis;

namespace ecommerece.Controllers
{
    public class WishlistsController : Controller
    {
        private readonly ecomereceContext _context;

        public WishlistsController(ecomereceContext context)
        {
            _context = context;
        }

        // GET: Wishlists
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var userId = _context.UserAdmins.Where(u => u.UserName == HttpContext.Session.GetString("UserName")).FirstOrDefault()?.UserId;
                if (userId != null)
                {
                    var ProductWishIds = await _context.Wishlists.Where(w => w.CustId == userId).Select(w => w.ProductId).ToListAsync();
                    var wishProducts = await _context.Products.Where(p => ProductWishIds.Contains(p.ProductId)).ToListAsync();
                    return View(wishProducts);
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<string> Index(string wishcart)
        {
            
                if (wishcart != null && wishcart != "")
                {
                    List<int> WishList = JsonConvert.DeserializeObject<List<int>>(wishcart);
                    string FinalList = "";
                    foreach (var item in WishList)
                    {
                        var WishProduct = await _context.Products.Where(prod => prod.ProductId == item).FirstOrDefaultAsync();
                        var Separate = WishProduct.ProductImg.Split(",");
                        FinalList += "          <li class='list-group-item d-flex justify-content-between lh-sm'>" +
                        "            <div>" +
                        "                <img class = 'img img-thumbnail' src = '" + Separate[0] + "' style = 'max-height:50px; max-width: 50px;' " +
                        "              <h6 class='my-0'>" + WishProduct.ProductName + "</h6><br/>" +
                        "              <small class='text-muted'>" + WishProduct.ShortDecsc + "</small>" +
                        "            </div>" +
                        "            <span class='text-muted'>" + WishProduct.ProductPrice + "</span>" +
                        "            <button style='max-height:50%' class='btn btn-danger delete' data-productid="+ item+">Remove Item</button>   "+
                        "          </li>";
                    }
                    return FinalList;
                }

            
            return string.Empty;
        }


        // GetCustomerId action for retrieving customerid which is logged in
        //public int GetCustomerId(string username)
        //{
        //    var CustomerId = _context.UserAdmins.Where(user=>user.UserName == username).FirstOrDefault();

        //    return CustomerId.UserId ;
        //}

        // GET: Wishlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Wishlists == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlists
                .FirstOrDefaultAsync(m => m.WishlistId == id);
            if (wishlist == null)
            {
                return NotFound();
            }

            return View(wishlist);
        }

        // GET: Wishlists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wishlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<JsonResult> Create(string username, string product)
        {
            List<int> wishItems = JsonConvert.DeserializeObject<List<int>>(product);

            var user = _context.UserAdmins.Where(user => user.UserName == username).FirstOrDefault();
            List<int> WishProductIds =await _context.Wishlists.Where(id => id.CustId == user.UserId).Select(pro => pro.ProductId).ToListAsync();
            if (user == null)
            {
                return Json(new { success = false });
            }

            foreach (var id in wishItems)
            {
                if (WishProductIds.Contains(id))
                {
                    // Alert the user that the product is already in their wishlist
                    TempData["AlertMessage"] = "Product " + id + " is already in your wishlist.";
                }
                else
                {
                    var wishlist = new Wishlist
                    {
                        CustId = user.UserId,
                        ProductId = id
                    };
                    _context.Add(wishlist);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }


        // GET: Wishlists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Wishlists == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlists.FindAsync(id);
            if (wishlist == null)
            {
                return NotFound();
            }
            return View(wishlist);
        }

        // POST: Wishlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WishlistId,ProductId,CustId,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Wishlist wishlist)
        {
            if (id != wishlist.WishlistId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wishlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WishlistExists(wishlist.WishlistId))
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
            return View(wishlist);
        }

        // GET: Wishlists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Wishlists == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlists
                .FirstOrDefaultAsync(m => m.WishlistId == id);
            if (wishlist == null)
            {
                return NotFound();
            }

            return View(wishlist);
        }

        // POST: Wishlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Wishlists == null)
            {
                return Problem("Entity set 'ecomereceContext.Wishlists'  is null.");
            }
            var wishlist = await _context.Wishlists.FindAsync(id);
            if (wishlist != null)
            {
                _context.Wishlists.Remove(wishlist);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishlistExists(int id)
        {
          return _context.Wishlists.Any(e => e.WishlistId == id);
        }
    }
}
