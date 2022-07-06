using ARMI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARMI.Services
{
    public class SystemService: ISystemService
    {
        private ApplicationDbContext db;

        public SystemService(ApplicationDbContext context)
        {
            db = context;
        }
        public IEnumerable<Models.System> Systems()
        {
            return db.Systems.OrderBy(x => x.Name).ToList();
        }
    }
}
