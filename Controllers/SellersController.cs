using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ecommerece.Models;
using System.Net.Mail;
using System.Net;

namespace ecommerece.Controllers
{
    public class SellersController : Controller
    {
        private readonly ecomereceContext _context;
        private readonly IWebHostEnvironment _img;

        public SellersController(ecomereceContext context, IWebHostEnvironment SellerImg)
        {
            _context = context;
            _img = SellerImg;
        }

        // GET: Sellers
        public IActionResult Index()

        {
            ViewBag.List = _context.Sellers.ToList().Count();
              return View( _context.Sellers.ToList());
        }

        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.SellerId == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // GET: Sellers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SellerId,SellerName,SellerPhone,SellerEmail,SellerAddress,CompanyName,WebsiteUrl,SellerCnic,City,ShortDesc,LongDesc,SellerGender,SellerDob,SystemUserId,Type,MaritalStatus,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,MetaData,SeoData,SellerImg")] 
        Seller seller, IFormFile sellerPP)
        {
            string GetExt = Path.GetExtension(sellerPP.FileName);
            string GiveName = Guid.NewGuid().ToString();
            string FinalPath = "/data/img/sellerpp/" + GiveName + GetExt;
            using (FileStream image = new FileStream(_img.WebRootPath + FinalPath, FileMode.Create))
                sellerPP.CopyTo(image);

            if (ModelState.IsValid)
            {
                seller.SellerImg = FinalPath;
                _context.Add(seller);
                await _context.SaveChangesAsync();

                // send email to new user

                if (!string.IsNullOrEmpty(seller.SellerEmail))
                {
                    MailMessage oEmail = new MailMessage();
                    oEmail.Subject = "Dear <b>"+ seller.SellerName +"<b>, Welcome to Theta Ecommerce.";
                    oEmail.Body = "Dear "+ seller.SellerName +" welcome to our Theta eShop." +
                        "You are our precious customer. Feel free to do shoping with us. Best of luck";
                    oEmail.To.Add(seller.SellerEmail);
                    oEmail.CC.Add("malikfazal44@hotmail.com");
                    oEmail.Bcc.Add("malikfazal44@gmail.com");


                    oEmail.From = new MailAddress("malikfazal44@hotmail.com", "Theta soltuions");
                    oEmail.Attachments.Add(new Attachment(_img.WebRootPath + seller.SellerImg));

                    SmtpClient oSMTP = new SmtpClient();
                    oSMTP.Port = 465;
                    oSMTP.Host = "mail.thetademos.com";
                    oSMTP.Credentials = new NetworkCredential("students@thetademos.com", "P@kist@n@@123");
                    oSMTP.EnableSsl = true;

                    try
                    {
                        oSMTP.Send(oEmail);
                    }
                    catch(Exception ex)
                    {

                    }
                    

                }


                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        // GET: Sellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SellerId,SellerName,SellerPhone,SellerEmail,SellerAddress,CompanyName,WebsiteUrl,SellerCnic,City,ShortDesc,LongDesc,SellerGender,SellerDob,SystemUserId,Type,MaritalStatus,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,MetaData,SeoData,SellerImg")] Seller seller)
        {
            if (id != seller.SellerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerExists(seller.SellerId))
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
            return View(seller);
        }

        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.SellerId == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sellers == null)
            {
                return Problem("Entity set 'ecomereceContext.Sellers'  is null.");
            }
            var seller = await _context.Sellers.FindAsync(id);
            if (seller != null)
            {
                _context.Sellers.Remove(seller);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerExists(int id)
        {
          return _context.Sellers.Any(e => e.SellerId == id);
        }
    }
}
