using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ARMI.Models
{
    //#Name;Title;Emulator;CloneOf;Year;Manufacturer;Category;Players;Rotation;Control;Status;DisplayCount;DisplayType;AltRomname;AltTitle;Extra;Buttons
    public class Rom
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
        public ICollection<RomListRoms> RomListRoms { get; set; }
    }
}
