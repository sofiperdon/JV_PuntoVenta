using JV_PuntoVenta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // Agregar este using para usar [Authorize]
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication; // Agregar este using para usar ILogger

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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "Home"); 
            }
            return Login();
        }

        public IActionResult Home()
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

        public IActionResult Ingresos()
        {
            return RedirectToAction("Index", "Ingresos");
        }

        public IActionResult Privacy()
        {
            return View();
        }

         // Añadir atributo de autorización
        public IActionResult AccesoRestringido()
        {
            return View();
        }

        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/" });
        }

        public IActionResult Logout()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" }, "Cookies", "oidc");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
        }
    }
}