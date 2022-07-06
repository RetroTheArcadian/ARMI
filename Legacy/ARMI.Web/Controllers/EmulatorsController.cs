using System.Collections.Generic;
using ARMI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ARMI.Web.Controllers
{   
    [Produces("application/json")]
    [Route("api/emulators")]
    public class EmulatorController : Controller
    { 
        private readonly IEmulatorService EmulatorService;

        public EmulatorController(IEmulatorService emulatorService)
        {
            EmulatorService = emulatorService;
        }    
        [HttpGet]
        [Route("emulators")]
        public IEnumerable<Models.Emulator> Emulators()
        {
            return EmulatorService.Emulators();
        }
    }
}
