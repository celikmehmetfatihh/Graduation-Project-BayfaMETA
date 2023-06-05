using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class PositionDto : BaseDto
    {
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public int NumberOfPeople { get; set; }
        public float TechnicalTestMultiplier { get; set; }
        public float VideoInterviewMultiplier { get; set; }
        public float ResumeMultiplier { get; set; }
        public bool IsAvailable { get; set; }
        public int? StageOneThreshold { get; set; }
        public bool? IsClosed { get; set; }

    }
}
