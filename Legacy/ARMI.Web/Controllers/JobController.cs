using ARMI.Models;
using ARMI.Services.Jobs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ARMI.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/job")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }
        [HttpGet]
        [Route("getjobs")]
        public async Task<List<Job>> GetJobs()
        {
            return await _jobService.GetJobs();
        }

        [HttpGet]
        [Route("getrunningjobs")]
        public async Task<List<Job>> GetRunningJobs()
        {
            return await _jobService.GetRunningJobs();
        }

        [HttpGet]
        [Route("canceljob")]
        public string CancelJob(string jobId)
        {
            var jobIdGuid = new Guid(jobId);
            return _jobService.CancelJob(jobIdGuid);
        }
    }
}
