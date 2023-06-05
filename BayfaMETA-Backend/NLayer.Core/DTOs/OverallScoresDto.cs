using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class OverallScoresDto
    {
        public float ResumeMatching { get; set; }
        public float ResumeTotalScore { get; set; }
        public float VideoScore { get; set; }
        public float TechnicalScore { get; set; }
        public float OverallScore { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string TelNo { get; set; }

    }
}
