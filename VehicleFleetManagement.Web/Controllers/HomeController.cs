using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VehicleFleetManagement.DAL.EF;
using VehicleFleetManagement.Web.Models;
using static System.Formats.Asn1.AsnWriter;

namespace VehicleFleetManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        //private readonly UserManager<ApplicationUser> _userManager;

        //public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager) {
        //    _userManager = userManager;
        //    _logger = logger;
        //}
        public IActionResult Index ()
        {
            //We need to check role to redirect user WHEN HE IS LOGGED IN (cookies)
            if (User.IsInRole("Owner")) {
                return RedirectToAction("Index", "Owner");
            }
            //We need to check role to redirect user WHEN HE IS LOGGED IN (cookies)
            if (User.IsInRole("Client")) {
                return RedirectToAction("Index", "Client");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //private readonly ApplicationDbContext _context;
        //public IActionResult GetUsers ()
        //{
        //  var users = _context.Users.ToList();
        //  Console.WriteLine($"Znaleziono {users.Count} u¿ytkowników.");

        //   return Json(users);
        //}

    }
}
