using System.Diagnostics;
using ITS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Log the access to the Index action
            _logger.LogInformation("Accessed the Index action.");

            // User is authenticated, return the Index view
            return View(); // Simplified to return the view matching the action name
        }

        [AllowAnonymous] // Allow unauthenticated users to access the Error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            // Log the error details
            _logger.LogError("An error occurred with Request ID: {RequestId}", errorViewModel.RequestId);

            return View(errorViewModel);
        }
    }
}