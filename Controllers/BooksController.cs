using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "The Pragmatic Programmer", Author = "Andrew Hunt", Year = 1999, Tags = new[] { "programming", "software", "best-practices" } },
                new Book { Id = 2, Title = "Clean Code", Author = "Robert C. Martin", Year = 2008, Tags = new[] { "programming", "clean-code", "architecture" } },
                new Book { Id = 3, Title = "Design Patterns", Author = "Gamma et al.", Year = 1994, Tags = new[] { "patterns", "oop" } }
            };

            // Serialize books to JSON and keep it in an object-typed local so you can open the JSON Visualizer while debugging.
            string booksJson = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            object booksObject = booksJson; // Inspect this local in the debugger and open the JSON Visualizer

            // Optionally expose JSON to the view as well
            ViewData["BooksJson"] = booksJson;

            return View(books);
        }
    }
}
