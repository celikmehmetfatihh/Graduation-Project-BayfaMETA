using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    /// <summary>
    /// Class for configuring async video interview part scoring of a position.
    /// </summary>
    public class VideoInterviewConfiguration : Base
    {
        public float? OpennessMultiplier { get; set; }
        public float? ConscientiousnessMultiplier { get; set; }
        public float? ExtraversionMultiplier { get; set; }
        public float? AgreeablenessMultiplier { get; set; }
        public float? NeuroticismMultiplier { get; set; }
        public string? InterviewTopics { get; set; }
        public int? AllocatedTime { get; set; }
        public Position? Position { get; set; }
        public int? PositionId { get; set; }
    }
}
