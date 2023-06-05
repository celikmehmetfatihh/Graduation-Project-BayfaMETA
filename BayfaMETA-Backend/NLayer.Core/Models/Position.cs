using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    /// <summary>
    /// Class for storing position details.
    /// A position has stage, technical question, video interview,
    /// resume configurations to keep track of the scoring details.
    /// Also a position have many resumes, video interviews of applicants.
    /// One position can have many users and a user can be in many positions.
    /// </summary>
    public class Position : Base
    {
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public int NumberOfPeople { get; set; }
        public int? StageOneThreshold { get; set; }
        public float TechnicalTestMultiplier { get; set; }
        public float VideoInterviewMultiplier { get; set; }
        public float ResumeMultiplier { get; set; }
        public bool IsAvailable { get; set; }
        public bool? IsClosed { get; set; }
        public DateTime FirstStageEndDate { get; set; }
        public DateTime SecondStageEndDate { get; set; }
        public ICollection<UserPosition> userPositions { get; set; }
        public ICollection<VideoInterview> videoInterviews { get; set; }
        public TechnicalConfiguration? TechnicalConfiguration { get; set; }
        public VideoInterviewConfiguration? VideoInterviewConfiguration { get; set; }
        public ResumeConfiguration? ResumeConfiguration { get; set; }
        public StageConfiguration? StageConfiguration { get; set; }
    }
}
