using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers  // Replace YourNamespace with your actual project namespace
{
    public class HomeController : Controller
    {
        // Action for the Index page
        public IActionResult Index()
        {
            return View();  // This will load the default Index page
        }

        // Action for the Choferes page
        public IActionResult Choferes()
        {
            return View();  // Render the Choferes page
        }

        // Action for the Buses page
        public IActionResult Buses()
        {
            return View();  // Render the Buses page
        }

        // Action for the Viajes page
        public IActionResult Viajes()
        {
            return View();  // Render the Viajes page
        }

        // Action for the Otros page
        public IActionResult Otros()
        {
            return View();  // Render the Otros page
        }
    }
}
