using System.Collections.Generic;

namespace ARMI.Dtos.ScraperDtos
{
    public class ArcadeDbDto
    {
        public int release { get; set; }
        public List<ArcadeDbResultDto> result { get; set; }
    }
}
