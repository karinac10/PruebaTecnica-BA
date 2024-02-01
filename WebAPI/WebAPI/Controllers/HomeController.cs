using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAPI.Models;

namespace WebAPI.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ListadoTarjetas()
        {
            return View();
        }
        public IActionResult EstadoCuenta()
        {
            return View();
        }
        public IActionResult RegistroPago()
        {
            return View();
        }
        public IActionResult RegistroCompra()
        {
            return View();
        }
        public IActionResult Transacciones()
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
