using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ecommerece.Models;
using ecommerece.Models.ViewModels;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ecommerece.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ecomereceContext _context;

        public OrdersController(ecomereceContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
              return View(await _context.Orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }
        //checkout 
        [HttpPost]
        public string CheckoutItems(string cart)
        {
            List<OrderItems> orderItems = JsonConvert.DeserializeObject<List<OrderItems>>(cart);
            string FinalCart = "";
            decimal Total = 0;
            foreach (var items in orderItems)
            {

                var Product = _context.Products.FirstOrDefault(i => i.ProductId == items.productId);
                var separate = Product.ProductImg.Split(',');
                FinalCart += "          <li class='list-group-item d-flex justify-content-between lh-sm'>" +
                    "            <div>" +
                    "                <img class = 'img img-thumbnail' src = '" + separate[0] + "' style = 'max-height:50px; max-width: 50px;' " +
                    "              <h6 class='my-0'>" + Product.ProductName + "</h6><br/>" +
                    "              <small class='text-muted'>" + Product.ShortDecsc + "</small>" +
                    "               <div class='qty-div'>" +
                    "                   <button class='qty-decrese' style='background:none; border:none; max-width:100px'>-</button>" +
                    "                   <span><input class='qty-counter' style='display:block; background:none; border:none' name='update[]' type='text' value='"+items.quantity+"' /></span>" +
                    "                   <button data-productId="+ items.productId +" class='qty-increase' style='background:none; border:none'>+</button>" +
                    "               </div>" +
                    "            </div>" +
                    "            <span class='text-muted'>" + Product.ProductPrice + "</span>" +
                    "          </li>";
                Total += (Product.ProductPrice ?? 0) * items.quantity;
            }
            FinalCart += "<li class='list-group-item d-flex justify-content-between'>" +
                "            <span>Total (PKR)</span>" +
                "            <strong>" + Total + "</strong>" +
                "          </li>";
            return FinalCart;
        }


        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CustId,Remarks,OrderPrice,PaymentMethod,OrderDiscount,FinalPrice,Status,MetaData,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustId,Remarks,OrderPrice,PaymentMethod,OrderDiscount,FinalPrice,Status,MetaData,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'ecomereceContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
