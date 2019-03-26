using System.Xml.Serialization;

namespace ARMI.Dtos.EmulationStation
{
    [XmlRoot(ElementName = "game")]
    public class EmulationStationGameDto
    {
        [XmlElement(ElementName = "desc")]
        public string Desc { get; set; }
        [XmlElement(ElementName = "developer")]
        public string Developer { get; set; }
        [XmlElement(ElementName = "genre")]
        public string Genre { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "image")]
        public string Image { get; set; }
        [XmlElement(ElementName = "lastplayed")]
        public string Lastplayed { get; set; }
        [XmlElement(ElementName = "marquee")]
        public string Marquee { get; set; }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "path")]
        public string Path { get; set; }
        [XmlElement(ElementName = "playcount")]
        public string Playcount { get; set; }
        [XmlElement(ElementName = "players")]
        public string Players { get; set; }
        [XmlElement(ElementName = "publisher")]
        public string Publisher { get; set; }
        [XmlElement(ElementName = "rating")]
        public string Rating { get; set; }
        [XmlElement(ElementName = "releasedate")]
        public string Releasedate { get; set; }
        [XmlAttribute(AttributeName = "source")]
        public string Source { get; set; }
        [XmlElement(ElementName = "thumbnail")]
        public string Thumbnail { get; set; }
        [XmlElement(ElementName = "video")]
        public string Video { get; set; }

        public EmulationStationGameDto()
        {
            Desc = "";
            Developer = "";
            Genre = "";
            Id = "";
            Image = "";
            Lastplayed = "";
            Marquee = "";
            Name = "";
            Path = "";
            Playcount = "";
            Players = "";
            Publisher = "";
            Rating = "";
            Releasedate = "";
            Source = "";
            Thumbnail = "";
            Video = "";
        }
    }
}
