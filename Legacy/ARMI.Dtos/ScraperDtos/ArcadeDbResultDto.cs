using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARMI.Dtos.ScraperDtos
{
    public class ArcadeDbResultDto
    {
        public int index { get; set; }
        public string url { get; set; }
        public string game_name { get; set; }
        public string title { get; set; }
        public string cloneof { get; set; }
        public string manufacturer { get; set; }
        public string url_image_ingame { get; set; }
        public string url_image_title { get; set; }
        public string url_image_marquee { get; set; }
        public string url_image_cabinet { get; set; }
        public string url_image_flyer { get; set; }
        public string genre { get; set; }
        public int players { get; set; }
        public string year { get; set; }
        public string status { get; set; }
        public string history { get; set; }
        public string history_copyright_long { get; set; }
        public string history_copyright_short { get; set; }
        public string youtube_video_id { get; set; }
        public string url_video_shortplay { get; set; }
        public string url_video_shortplay_hd { get; set; }
        public int emulator_id { get; set; }
        public string emulator_name { get; set; }
        public string languages { get; set; }
        public int rate { get; set; }
    }
}