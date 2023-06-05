using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class VideoInterviewConfigurationDto : BaseDto
    {
        public float OpennessMultiplier { get; set; }
        public float ConscientiousnessMultiplier { get; set; }
        public float ExtraversionMultiplier { get; set; }
        public float AgreeablenessMultiplier { get; set; }
        public float NeuroticismMultiplier { get; set; }
        public string? InterviewTopics { get; set; }
        public int AllocatedTime { get; set; }
        public int PositionId { get; set; }
    }
}
