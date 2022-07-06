using ARMI.Enums;
using System.Collections.Generic;

namespace ARMI.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Title { get; set; }
        public ClientHostType ClientHostType { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsMaster { get; set; }
        public string RomFolder { get; set; }
        public string AttractModeFolder { get; set; }

        public ICollection<Emulator> Emulators { get; set; }
        public ICollection<RomList> RomLists { get; set; }
    }
}
