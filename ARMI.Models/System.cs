using System;
using System.Collections.Generic;

namespace ARMI.Models
{
    public class System
    {
        public int SystemId { get; set; }
        public string Name { get; set; }
        public string RomFolder { get; set; }
        public string ReleaseDate { get; set; }
        public string Developer { get; set; }
        public string Manufacturer { get; set; }
        public string Controllers { get; set; }
        public string Cpu { get; set; }
        public string Memory { get; set; }
        public string Graphics { get; set; }
        public string Sound { get; set; }
        public string Display { get; set; }
        public string Media { get; set; }
        public string Description { get; set; }
        public string FileExtensions { get;set;}
        public string Wiki { get; set; }
        public string ImageName { get; set; }

        public ICollection<Emulator> Emulators { get; set; }
    }
}
