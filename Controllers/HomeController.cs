using JV_PuntoVenta.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JV_PuntoVenta.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Productos()
        {
            return RedirectToAction("Index", "Productos");
        }

        public IActionResult Ventas()
        {
            return RedirectToAction("Index", "Ventas"); 
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
