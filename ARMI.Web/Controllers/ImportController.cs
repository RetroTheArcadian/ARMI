using ARMI.Services.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace ARMI.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/import")]
    public class ImportController : ControllerBase
    {
        private readonly IJobService _jobService;

        public ImportController(IJobService jobService)
        {
            _jobService = jobService;
           
        }

        [HttpGet]
        [Route("emulators")]
        public string ImportEmulators()
        {
            return _jobService.StartImportEmulators();
        }

        [HttpGet]
        [Route("romlists")]
        public string ImportRomLists()
        {
            return _jobService.StartImportRomLists();
        }

        [HttpGet]
        [Route("emgamelists")]
        public string ImportGameListXmlFromEmulationStation()
        {
            return _jobService.StartImportGameListXmlFromEmulationStation();
        }
    }
}
