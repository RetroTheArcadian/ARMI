using System.Collections.Generic;
using ARMI.Dtos;
using ARMI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ARMI.Web.Controllers
{   
    [Produces("application/json")]
    [Route("api/roms")]
    public class RomsController : Controller { 
        private readonly IRomService RomService;
        private readonly IFileService FileService;

        public RomsController(IRomService romService, IFileService fileService)
        {
            RomService = romService;
            FileService = fileService;
        } 
        
        [HttpGet]
        [Route("roms")]
        public IEnumerable<Models.Rom> Roms()
        {
            return RomService.Roms();
        }

        [HttpGet]
        [Route("romswithfileinfo")]
        public List<RomDto> RomsWithFileInfo(List<int> romListIds)
        {
            return FileService.RomsWithFileInfo(romListIds);
        }
    }
}
