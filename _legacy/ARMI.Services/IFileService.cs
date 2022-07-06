using ARMI.Dtos;
using System.Collections.Generic;

namespace ARMI.Services
{
    public interface IFileService
    {
        List<RomDto> RomsWithFileInfo(List<int> romListIds);
    }
}
