using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Sample variables for breakpoint debugging
            string appName = "WebApplication1";
            int requestCount = 0;
            var currentTime = DateTime.Now;
            var traceId = HttpContext.TraceIdentifier;
            var requestPath = Request.Path;
            
            // Simulate some processing
            for (int i = 0; i < 5; i++)
            {
                requestCount++;
            }
            
            // Create a model or data to pass to view using a named type so DebuggerDisplay is used
            var debugInfo = new DebugInfo
            {
                AppName = appName,
                RequestCount = requestCount,
                CurrentTime = currentTime,
                TraceId = traceId,
                RequestPath = requestPath
            };

            return View(debugInfo);
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
