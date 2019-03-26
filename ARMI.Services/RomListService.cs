using System.Collections.Generic;
using System.Linq;
using ARMI.Data;
using ARMI.Dtos;
using ARMI.Models;

namespace ARMI.Services
{
    public class RomListService:IRomListService
    {
        private ApplicationDbContext db;

        public RomListService(ApplicationDbContext context)
        {
            db = context;
        }
        public IEnumerable<RomList> RomLists()
        {
            return db.RomLists.OrderBy(x=>x.Title).Take(100).ToList();
        }

        public IEnumerable<TreeViewNodeDto> RomListsByParentId(int? parentRomListId)
        {
            var romListsAll = db.RomLists.ToList();
            var romLists = romListsAll.Where(x=>x.ParentRomListId.Equals(parentRomListId)).OrderBy(x => x.Title).ToList();
            
            return romLists.Select(romList => new TreeViewNodeDto
            {
                name = romList.Title,
                id = romList.RomListId,
                hasChildren = romListsAll.Any(c => c.ParentRomListId.Equals(romList.RomListId)),
            });
        }
    }
}
