using System.Collections.Generic;

namespace ARMI.Models
{
    public class RomList
    {
        public int RomListId { get; set; }
        public int? ParentRomListId { get; set; }
        public RomList ParentRomList { get; set; }
        public string Title { get; set; }

        public ICollection<RomList> SubRomLists { get; set; }
        public ICollection<RomListRoms> RomListRoms { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
