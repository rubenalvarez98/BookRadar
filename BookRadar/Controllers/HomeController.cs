using System.Diagnostics;
using BookRadar.Data;
using BookRadar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookRadar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: /Home/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: /Home/ProbarEF
        public async Task<IActionResult> ProbarEF()
        {
            var total = await _context.HistorialBusquedas.CountAsync();
            return Content($"EF Core funciona ✔. Registros en HistorialBusquedas: {total}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
