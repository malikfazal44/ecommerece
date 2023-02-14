using ecommerece.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ecommerece.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ecomereceContext _context;

        public HomeController(ILogger<HomeController> logger, ecomereceContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // this viewbag is used in layout page in navbar
            ViewBag.category = _context.Categories.ToList();
            // follwing viewbag is used in index of home for showing product
            ViewBag.ProductCard = _context.Products.Take(3).ToList();
            // following viewbag  is used in home index for taing category list
            ViewBag.CategoryFlipCard = _context.Categories.Take(3).ToList();
            return View();
        }

        // success view after creating account
        public IActionResult SuccessfulCreation()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}