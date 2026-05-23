using Microsoft.AspNetCore.Mvc;
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

        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // Read books from the database
            var books = _db.Books.ToList();

            // Serialize books to JSON and keep it in an object-typed local so you can open the JSON Visualizer while debugging.
            string booksJson = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            object booksObject = booksJson; // Inspect this local in the debugger and open the JSON Visualizer

            // Optionally expose JSON to the view as well
            ViewData["BooksJson"] = booksJson;

            return View(books);
        }
    }
}
