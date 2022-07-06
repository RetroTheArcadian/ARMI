using ARMI.Models;
using ARMI.Services;
using ARMI.Services.Scrapers;
using Coravel.Queuing.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARMI.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/scraper")]
    public class ScraperController : ControllerBase
    {
        private readonly IArcadeDbScraperService _arcadeDbScraperService;
        private readonly IImportService _importService;
        private readonly IQueue _queue;

        public ScraperController(
            IQueue queue, 
            IImportService importService,
            IArcadeDbScraperService arcadeDbScraperService
            )
        {
            _arcadeDbScraperService = arcadeDbScraperService;
            _importService = importService;
            _queue = queue;
        }
        [HttpGet]
        [Route("getdatafromromlist")]
        public string GetDataFromRomList(int romListId)
        {
            string jobId = Guid.NewGuid().ToString("N");
            _queue.QueueAsyncTask(() => _arcadeDbScraperService.GetDataFromRomList(romListId, jobId));
            return jobId;
        }
    }
}
