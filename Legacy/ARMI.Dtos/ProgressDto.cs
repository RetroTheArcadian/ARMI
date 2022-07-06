using ARMI.Enums;

namespace ARMI.Dtos
{
    public class ProgressDto
    {
        public int Percent { get; set; }
        public int Done { get; set; }
        public int Remaining { get; set; }
        public int Total { get; set; }
        public string MsgLine1 { get; set; }
        public string MsgLine2 { get; set; }
        public JobStatus JobStatus { get;set;}
    }
}
