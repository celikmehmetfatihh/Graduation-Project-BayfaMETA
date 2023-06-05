using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    /// <summary>
    /// Class for configuration of the resume scoring process of a position
    /// </summary>
    public class ResumeConfiguration : Base
    {
        public Position? Position { get; set; }
        public int? PositionId { get; set; }
        public string? JobPositions { get; set; }
        public string? RequiredSkills { get; set; }
        public float? MinExperience { get; set; }
        public float? EduMultiplier { get; set; }
        public float? ExpMultiplier { get; set; }
        public float? TechSkillsMultiplier { get; set; }
        public float? SoftSkillsMultiplier { get; set; }
        public float? KeywordMultiplier { get; set; }
    }
}
