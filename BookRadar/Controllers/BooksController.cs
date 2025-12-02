using System;
using System.Linq;
using System.Threading.Tasks;
using BookRadar.Data;
using BookRadar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookRadar.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Books/Buscar
        // Muestra el formulario de búsqueda de autor
        [HttpGet]
        public IActionResult Buscar()
        {
            return View(new BuscarAutorViewModel());
        }

        // POST: /Books/Buscar
        // Valida el formulario con DataAnnotations
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Buscar(BuscarAutorViewModel model)
        {
            if (!ModelState.IsValid)
            {
  
                return View(model);
            }

            return RedirectToAction("BuscarResultados", new { autor = model.Autor });
        }

        [HttpGet]
        public async Task<IActionResult> BuscarResultados(string autor)
        {
            if (string.IsNullOrWhiteSpace(autor))
            {
     
                return RedirectToAction("Buscar");
            }

            string url = $"https://openlibrary.org/search.json?author={Uri.EscapeDataString(autor)}";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return Content("No se pudo obtener información desde la API de OpenLibrary.");
            }

            var json = await response.Content.ReadAsStringAsync();

            var result = System.Text.Json.JsonSerializer.Deserialize<OpenLibraryResponse>(json);

            var libros = result?.Docs
                .Select(doc => new LibroResultadoViewModel
                {
                    Titulo = doc.Title,
                    AnioPublicacion = doc.FirstPublishYear,
                    Editorial = doc.Publisher?.FirstOrDefault()
                })
                .ToList() ?? new List<LibroResultadoViewModel>();

    
            foreach (var libro in libros)
            {
                var parametros = new[]
                {
                    new SqlParameter("@Autor", autor),
                    new SqlParameter("@Titulo", libro.Titulo ?? (object)DBNull.Value),
                    new SqlParameter("@AnioPublicacion", (object?)libro.AnioPublicacion ?? DBNull.Value),
                    new SqlParameter("@Editorial", (object?)libro.Editorial ?? DBNull.Value)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_InsertHistorialBusqueda @Autor, @Titulo, @AnioPublicacion, @Editorial",
                    parametros
                );
            }

            return View(libros);
        }


        [HttpGet]
        public async Task<IActionResult> Historial()
        {
            var historial = await _context.HistorialBusquedas
                .OrderByDescending(h => h.FechaBusqueda)
                .ToListAsync();

            return View(historial);
        }
    }
}
