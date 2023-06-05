using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    /// <summary>
    /// Class for configuring stage score effects of a position
    /// </summary>
    public class StageConfiguration : Base
    {
        public int? stageOneThreshold { get; set; }
        public int? VideoInterviewPercentage { get; set; }
        public int? technicalPercentage { get; set; }
        public int? cvPercentage { get; set; }
        public bool? VideoInterviewFlag { get; set; }
        public bool? technicalFlag { get; set; }
        public bool? cvFlag { get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
    }
}
