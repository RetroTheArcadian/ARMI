using ARMI.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ARMI.Services
{
    public interface IImportService
    {
        Task ImportEmulators(Job job, CancellationTokenSource cancellationTokenSource);
        Task ImportRomLists(Job job);
        Task FixParentKeys(Job job);
        Task ImportGameListXmlFromEmulationStation(Job job);
    }
}
