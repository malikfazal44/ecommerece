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
    public class CustomersController : Controller
    {
        private readonly ecomereceContext _context;
        private readonly IWebHostEnvironment _img;

        public CustomersController(ecomereceContext context, IWebHostEnvironment img)
        {
            _context = context;
            _img = img;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            ViewBag.List = _context.Customers.ToList().Count();
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustId,CustName,CustPhone,CustEmail,CustAddress,CustCity,CustDob,SystemUserId,CustGender,CustStatus,MetaData,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,CustImg")] 
        Customer customer, IFormFile custimg,string username,string password)
        {


            string FinalPath = "/data/img/custimg/" + Guid.NewGuid().ToString() + Path.GetExtension(custimg.FileName);
            using (FileStream prod = new FileStream(_img.WebRootPath + FinalPath, FileMode.Create))
            {
                custimg.CopyTo(prod);
            }
            

            if (ModelState.IsValid)
            {
                // add username & password to useradmin table and userID to useradmin & customer
                UserAdmin user = new UserAdmin();
                user.UserName = username;
                user.Password = password;
                user.Type = 4;
                _context.UserAdmins.Add(user);
                await _context.SaveChangesAsync();
                // get user id and assign this id inside customer table
                var GetUserId = _context.UserAdmins.Where(u => u.UserName == username).FirstOrDefault();

                customer.SystemUserId = GetUserId.UserId;
                customer.CustImg = FinalPath;
                _context.Add(customer);
                await _context.SaveChangesAsync();

                // after creation of customer sending him welcoming email
                if (!string.IsNullOrEmpty(customer.CustEmail))
                {
                    MailMessage oEmail = new MailMessage();
                    oEmail.Subject = "Dear <b>" + customer.CustName +"</b>, Welcome to Our Estore";
                    oEmail.Body = "Dear <b>" + customer.CustName + "</b>, welcome to Theta Estore." +
                        "You are very precious to us. feel free to do shopping with us. best of luck.";
                    oEmail.To.Add(customer.CustEmail);
                    oEmail.CC.Add("malikfazal44@hotmail.com");
                    oEmail.Bcc.Add("malikfazal44@hotmail.com");

                    oEmail.From = new MailAddress("malikfaza44@gmail.com", "Theta Ecommerce");
                    oEmail.Attachments.Add(new Attachment(_img.WebRootPath+customer.CustImg));

                    SmtpClient oSMTP = new SmtpClient();
                    oSMTP.Port = 465;
                    oSMTP.Host = "mail.thetademos.com";
                    oSMTP.Credentials = new NetworkCredential("students@thetademos.com", "P@kist@n@@123");
                    oSMTP.EnableSsl = true;
                    try
                    {
                        oSMTP.Send(oEmail);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                return RedirectToAction("SuccessfulCreation","Home");
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustId,CustName,CustPhone,CustEmail,CustAddress,CustCity,CustDob,SystemUserId,CustGender,CustStatus,MetaData,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,CustImg")]
        Customer customer, IFormFile custimg)
        {
            if (id != customer.CustId)
            {
                return NotFound();
            }

            using (FileStream prod = new FileStream(_img.WebRootPath + "/data/img/custimg/" + Guid.NewGuid().ToString() + Path.GetExtension(custimg.FileName), FileMode.Create))
            {
                custimg.CopyTo(prod);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customer.CustImg = _img.WebRootPath + "/data/img/custimg/" + Guid.NewGuid().ToString() + Path.GetExtension(custimg.FileName);
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustId))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'ecomereceContext.Customers'  is null.");
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                var FilePath = customer.CustImg;
                string FinalPath = _img.WebRootPath + FilePath;
                FileInfo File = new FileInfo(FinalPath);
                if (File.Exists)
                {
                    File.Delete();
                }
                _context.Customers.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // delete through ajax
        [HttpPost]
        public bool DeleteAjax(int id)
        {
            try
            {
                _context.Customers.Remove(_context.Customers.Find(id));
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
            
        }

        private bool CustomerExists(int id)
        {
          return _context.Customers.Any(e => e.CustId == id);
        }
    }
}
