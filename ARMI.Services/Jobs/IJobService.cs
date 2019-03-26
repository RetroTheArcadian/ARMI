using ARMI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ARMI.Services.Jobs
{
    public interface IJobService
    {
        string StartImportEmulators();        
        string StartImportRomLists();
        string StartImportGameListXmlFromEmulationStation();
        string CancelJob(Guid guid);
        Task<List<Job>> GetJobs();
        Task<List<Job>> GetRunningJobs();
        Task<List<Job>> GetJobs(string search, int pageNo, int pageSize, string sortColumn, bool sortByAscending);
    }
}
