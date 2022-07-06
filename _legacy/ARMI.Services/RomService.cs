using System.Collections.Generic;
using System.Linq;
using ARMI.Data;
using ARMI.Models;

namespace ARMI.Services
{
    public class RomService : IRomService
    {
        private ApplicationDbContext db;

        public RomService(ApplicationDbContext context)
        {
            db = context;
        }
        public IEnumerable<Rom> Roms()
        {
            return db.Roms.OrderBy(x=>x.Name).Take(100).ToList();
        }
    }
}
