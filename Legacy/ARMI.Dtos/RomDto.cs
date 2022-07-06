using ARMI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARMI.Dtos
{
    public class RomDto
    {
        public int RomId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string CloneOf { get; set; }
        public string Year { get; set; }
        public string Manufacturer { get; set; }
        public string Category { get; set; }
        public string Players { get; set; }
        public string Rotation { get; set; }
        public string Control { get; set; }
        public string Status { get; set; }
        public string DisplayCount { get; set; }
        public string DisplayType { get; set; }
        public string AltRomname { get; set; }
        public string AltTitle { get; set; }
        public string Extra { get; set; }
        public string Buttons { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedDate { get; set; }

        public int? EmulatorId { get; set; }
        public int? RomListId { get; set; }
        public Emulator Emulator { get; set; }
        public string EmulatorNameOrg { get; set; }
        public IList<RomListRoms> RomListRoms { get; set; }

        public bool HaveRomFile { get; set; }
        public bool HaveBoxartFile { get; set; }
        public bool HaveCartartFile { get; set; }
        public bool HaveFanartFile { get; set; }
        public bool HaveFlyerFile { get; set; }
        public bool HaveMarqueeFile { get; set; }
        public bool HaveSnapFile { get; set; }
        public bool HaveWheelFile { get; set; }
    }
}
