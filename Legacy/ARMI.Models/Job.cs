using ARMI.Enums;
using System;

namespace ARMI.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public string JobIdGuid { get; set; }
        public string Title { get; set; }
        public JobStatus JobStatus { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
