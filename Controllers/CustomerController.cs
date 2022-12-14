using ecommerece.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecommerece.Controllers
{
    public class CustomerController : Controller
    {
        public ecomereceContext CustomerGlob = null;

        public CustomerController(ecomereceContext _EF)
        {
            CustomerGlob = _EF;
        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer obj)
        {
            CustomerGlob.Customers.Add(obj);
            await CustomerGlob.SaveChangesAsync();
            return RedirectToAction(nameof(CreateCustomer));
        }
        

    }
}
