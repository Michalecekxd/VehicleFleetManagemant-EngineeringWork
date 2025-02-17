using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleFleetManagement.DAL.EF;
using ILogger = Castle.Core.Logging.ILogger;


namespace VehicleFleetManagement.Web.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index() 
        {
            return View();
        }
    }
}
