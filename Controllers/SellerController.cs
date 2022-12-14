using ecommerece.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecommerece.Controllers
{
    public class SellerController : Controller
    {
        public ecomereceContext SellerGlobe = null;
        public SellerController(ecomereceContext _EFSeller)
        {
            SellerGlobe = _EFSeller;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateSeller()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSeller(Seller obj)
        {
            SellerGlobe.Sellers.Add(obj);
            await SellerGlobe.SaveChangesAsync();
            return RedirectToAction(nameof(CreateSeller));
        }
    }
}
