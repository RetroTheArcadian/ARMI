namespace ARMI.Models
{
    public class RomListRoms
    {
        public int RomListId { get; set; }
        public RomList RomList { get; set; }

        public int RomId { get; set; }      
        public Rom Rom { get; set; }
    }
}
