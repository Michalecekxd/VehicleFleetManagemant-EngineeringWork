using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using ZstdSharp.Unsafe;

namespace VehicleFleetManagement.Web.Controllers
{
    // Use this Controller to share common methods (for example _logger(_logger is used for recording errors, information, warnings) will be used in all controllers) 
    public class BaseController : Controller    
    {
        private readonly ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }
    }
}
