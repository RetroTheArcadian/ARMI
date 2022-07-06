using System.Collections.Generic;
using ARMI.Dtos;
using ARMI.Models;
using ARMI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ARMI.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/romlists")]
    public class RomListController : Controller
    { 
        private readonly IRomListService RomListService;

        public RomListController(IRomListService romListService)
        {
            RomListService = romListService;
        }
        [HttpGet]
        [Route("romlists")]
        public IEnumerable<RomList> RomLists()
        {
            return RomListService.RomLists();
        }
        [HttpGet("{parentRomListId}")]
        [Route("romlistsbyparent")]
        public IEnumerable<TreeViewNodeDto> RomListsByParentId(int parentRomListId)
        {
            int? parentRomListIdNullable = parentRomListId == -1 ? null : (int?)parentRomListId;
            return RomListService.RomListsByParentId(parentRomListIdNullable);
        }
    }
}
