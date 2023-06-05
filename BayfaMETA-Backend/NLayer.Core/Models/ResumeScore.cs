using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    /// <summary>
    /// Class for storing resume score details which contains
    /// both matching and total score.
    /// Total score also considers content of a resume.
    /// </summary>
    public class ResumeScore : Base
    {
        public double MatchingScore { get; set; }
        public double TotalScore { get; set; }
        public Resume Resume { get; set; }
        public int ResumeId { get; set; }
    }
}
