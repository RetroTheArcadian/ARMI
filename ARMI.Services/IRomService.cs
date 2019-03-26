using ARMI.Models;
using System.Collections.Generic;

namespace ARMI.Services
{
    public interface IRomService
    {
        IEnumerable<Rom> Roms();
    }
}
