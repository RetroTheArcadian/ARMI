using System.Collections.Generic;
using System.Linq;
using ARMI.Data;
using ARMI.Models;

namespace ARMI.Services
{
    public class EmulatorService : IEmulatorService
    {
        private ApplicationDbContext db;

        public EmulatorService(ApplicationDbContext context)
        {
            db = context;
        }
        public IEnumerable<Emulator> Emulators()
        {
            return db.Emulators.ToList();
        }
    }
}
