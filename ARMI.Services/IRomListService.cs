using ARMI.Dtos;
using ARMI.Models;
using System.Collections.Generic;

namespace ARMI.Services
{
    public interface IRomListService
    {
        IEnumerable<RomList> RomLists();
        IEnumerable<TreeViewNodeDto> RomListsByParentId(int? parentRomListId);
    }
}
