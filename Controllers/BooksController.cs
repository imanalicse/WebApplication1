using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<BooksController> _logger;

        public BooksController(ApplicationDbContext db, ILogger<BooksController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Read books from the database
            var books = _db.Books.ToList();

            // Test log entry to verify Serilog is configured
            _logger.LogInformation("Books.Index called, found {Count} books", books.Count);

            // Serialize books to JSON and keep it in an object-typed local so you can open the JSON Visualizer while debugging.
            string booksJson = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            object booksObject = booksJson; // Inspect this local in the debugger and open the JSON Visualizer

            // Optionally expose JSON to the view as well
            ViewData["BooksJson"] = booksJson;

            return View(books);
        }
    }
}
