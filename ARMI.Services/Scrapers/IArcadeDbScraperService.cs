using ARMI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARMI.Services.Scrapers
{
    public interface IArcadeDbScraperService
    {
        Task GetDataFromRomList(int romListId, string jobId);
    }
}
