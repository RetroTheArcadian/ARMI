using System.Collections.Generic;
using ARMI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ARMI.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService ClientService;

        public ClientController(IClientService clientService)
        {
            ClientService = clientService;
        }
        [HttpGet]
        [Route("clients")]
        public IEnumerable<Models.Client> Clients()
        {
            return ClientService.Clients();
        }
    }
}