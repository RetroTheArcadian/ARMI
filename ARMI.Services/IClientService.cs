using ARMI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARMI.Services
{
    public interface IClientService
    {
        IEnumerable<Client> Clients();
    }
}
