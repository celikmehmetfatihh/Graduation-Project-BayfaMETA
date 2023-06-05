using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class ResumeDto : BaseDto
    {
        public int? PositionId { get; set; }
        public int UserId { get; set; }
        public string? TechnicalSkills { get; set; }
        public string? SoftSkills { get; set; }
        public string? Experience { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Education { get; set; }
        public double MatchingScore { get; set; }
        public double TotalScore { get; set; }
    }
}
