using System.Collections.Generic;

namespace ARMI.Services
{
    public interface ISystemService
    {
        IEnumerable<Models.System> Systems();
    }
}
