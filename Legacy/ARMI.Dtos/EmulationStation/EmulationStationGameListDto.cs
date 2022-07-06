using System.Collections.Generic;
using System.Xml.Serialization;

namespace ARMI.Dtos.EmulationStation
{
    [XmlRoot(ElementName = "gameList")]
    public class EmulationStationGameListDto
    {
        [XmlElement(ElementName = "game")]
        public List<EmulationStationGameDto> Game { get; set; }

        public string FolderName { get; set; }
    }
}
