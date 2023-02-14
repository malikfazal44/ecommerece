using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ecommerece.Models;
using System.Net.Mail;

namespace ecommerece.Controllers
{
    public class UserAdminsController : Controller
    {
        private readonly ecomereceContext _context;

        public UserAdminsController(ecomereceContext context)
        {
            _context = context;
        }

        // Login to system
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserAdmin user)
        {
            UserAdmin Found = _context.UserAdmins.Where(oUser => oUser.UserName == user.UserName && oUser.Password == user.Password).FirstOrDefault();
            if (Found == null)
            {
                ViewBag.Error = "Incorrect Username or Password";
                return View();
            }

            //if successfull login
            HttpContext.Session.SetString("UserName", Found.UserName);
            HttpContext.Session.SetInt32("UserId", Found.UserId);
            HttpContext.Session.SetInt32("type", Found.Type.Value);
            return RedirectToAction("Index", "Products");
        }
        // forgot password action
        public IActionResult ForgotPassword(string forgotpassword, UserAdmin useradmin)
        {
            var Find_Userid = _context.UserAdmins.Where(uName => uName.UserName == forgotpassword).FirstOrDefault();
            if(Find_Userid.Type == 3)
            {
                var Find_seller = _context.Sellers.Where(s => s.SystemUserId == Find_Userid.UserId).FirstOrDefault();

                // generate reset link
                string resetToken = Guid.NewGuid().ToString();
                useradmin.Token = resetToken;
                _context.Add(useradmin);
                _context.SaveChanges();
                // useradmin.Timestamp = DateTime.UtcNow();
                // send reset link to registered mail address
                if (!string.IsNullOrEmpty(Find_seller.SellerEmail))
                {
                    MailMessage resetMail = new MailMessage();
                    resetMail.Subject = "Reset Your Password";
                    resetMail.Body = "Dear <b>" + useradmin.UserName + "</b> click the following " +
                        "link to reset your password. <br />" +
                        "<a href=''>" + resetToken + "</a>";
                }
                
            }
            return View();
        }

        // password reset page
        public IActionResult PasswordReset()
        {
            return View();
        }

        //Logout 
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login","UserAdmins");
        }

        // GET: UserAdmins
        public async Task<IActionResult> Index()
        {

              return View(await _context.UserAdmins.ToListAsync());
        }

        // GET: UserAdmins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserAdmins == null)
            {
                return NotFound();
            }

            var userAdmin = await _context.UserAdmins
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userAdmin == null)
            {
                return NotFound();
            }

            return View(userAdmin);
        }

        // GET: UserAdmins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserAdmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,Password,Type,Status,MetaData,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] UserAdmin userAdmin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userAdmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View(userAdmin);
        }

        // GET: UserAdmins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserAdmins == null)
            {
                return NotFound();
            }

            var userAdmin = await _context.UserAdmins.FindAsync(id);
            if (userAdmin == null)
            {
                return NotFound();
            }
            return View(userAdmin);
        }

        // POST: UserAdmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,Password,Type,Status,MetaData,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] UserAdmin userAdmin)
        {
            if (id != userAdmin.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAdmin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAdminExists(userAdmin.UserId))
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
            return View(userAdmin);
        }

        // GET: UserAdmins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserAdmins == null)
            {
                return NotFound();
            }

            var userAdmin = await _context.UserAdmins
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userAdmin == null)
            {
                return NotFound();
            }

            return View(userAdmin);
        }

        // POST: UserAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserAdmins == null)
            {
                return Problem("Entity set 'ecomereceContext.UserAdmins'  is null.");
            }
            var userAdmin = await _context.UserAdmins.FindAsync(id);
            if (userAdmin != null)
            {
                _context.UserAdmins.Remove(userAdmin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAdminExists(int id)
        {
          return _context.UserAdmins.Any(e => e.UserId == id);
        }
    }
}
