using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class StageConfigurationDto : BaseDto
    {
        public int? StageOneThreshold { get; set; }
        public int? VideoInterviewPercentage { get; set; }
        public int? TechnicalPercentage { get; set; }
        public int? CVPercentage { get; set; }
        public bool? VideoInterviewFlag { get; set; }
        public bool? TechnicalFlag { get; set; }
        public bool? CVFlag { get; set; }
        public int PositionId { get; set; }
    }
}
