using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{   
    /// <summary>
    /// Class for storing video interview score details.
    /// </summary>
    public class VideoInterviewScore:Base
    {
        public String? score_type { get; set; }
        public float score { get; set; }
        public String? details { get; set; }

        public float extraversion { get; set; }
        public float agreeableness { get; set; }
        public float openness { get; set; }
        public float conscientiousness { get; set; }
        public float neuroticism { get; set; }

        public int VideoInterviewId { get; set; }

        public VideoInterview VideoInterview { get; set; }
    }
}
