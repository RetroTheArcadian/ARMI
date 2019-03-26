using System;
using System.Collections.Generic;

namespace ARMI.Models
{
    public class Emulator
    {
        public int EmulatorId { get; set; }
        public string EmulatorName { get; set; }
        public string Executable { get; set; }
        public string Args { get; set; }
        public string Rompath { get; set; }
        public string Romext { get; set; }
        public string InfoSource { get; set; }
        public string Boxart { get; set; }
        public string Cartart { get; set; }
        public string Fanart { get; set; }
        public string Flyer { get; set; }
        public string Marquee { get; set; }
        public string Snap { get; set; }
        public string Wheel { get; set; }
        public DateTime ModifiedDate { get; set; }
        
        public int? SystemId { get; set; }
        public System System { get; set; }

        public string RomFolderName { get; set; }
        public ICollection<Rom> Roms { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
