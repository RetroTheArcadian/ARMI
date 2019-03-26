using System.Collections.Generic;

namespace ARMI.Services
{
    public interface IEmulatorService
    {
        IEnumerable<Models.Emulator> Emulators();
    }
}
