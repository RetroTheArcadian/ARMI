using System.Collections.Generic;
using ARMI.Services;
using Microsoft.AspNetCore.Mvc;


namespace ARMI.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/systems")]
    public class SystemController : Controller
    {
        private readonly ISystemService SystemService;

        public SystemController(ISystemService systemService)
        {
            SystemService = systemService;
        }

        [HttpGet]
        [Route("systems")]
        public IEnumerable<Models.System> Systems()
        {
            return SystemService.Systems();
        }
    }
}
