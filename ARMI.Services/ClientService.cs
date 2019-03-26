using ARMI.Data;
using ARMI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARMI.Services
{
    public class ClientService: IClientService
    {
        private ApplicationDbContext db;

        public ClientService(ApplicationDbContext context)
        {
            db = context;
        }
        public IEnumerable<Client> Clients()
        {
            return db.Clients.ToList();
        }
    }
}
