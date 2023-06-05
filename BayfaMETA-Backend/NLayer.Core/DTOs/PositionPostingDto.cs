using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class PositionPostingDto
    {
        public bool? IsAvailable { get; set; }
        //Position Information
        public string? JobTitle { get; set; }
        public string? JobDescription { get; set; }
        public int? NumberOfPeople { get; set; }
        public float? TechnicalTestMultiplier { get; set; }
        public float? VideoInterviewMultiplier { get; set; }
        public float? ResumeMultiplier { get; set; }
        public int? StageOneThreshold { get; set; }
        public bool? IsClosed { get; set; }

        //Resume Configuration
        public string? JobPositions { get; set; }
        public string? RequiredSkills { get; set; }
        public float? MinExperience { get; set; }
        public float? EduMultiplier { get; set;}
        public float? ExpMultiplier { get; set;}
        public float? TechSkillsMultiplier { get; set;}
        public float? SoftSkillsMultiplier { get; set;}
        public float? KeywordMultiplier { get; set; }


        //Video Interview Configuration
        public float? OpennessMultiplier { get; set;}
        public float? ConscientiousnessMultiplier { get; set;}
        public float? ExtraversionMultiplier { get; set;}
        public float? AgreeablenessMultiplier { get; set;}
        public float? NeuroticismMultiplier { get; set; }
        public string? InterviewTopics { get; set; }
        public int? AllocatedTime { get; set; }


        //Techical Question Configuration
        public string? ExcelPath { get; set; }
        public int QuestionTime { get; set; }



        public DateTime FirstStageEndDate { get; set; }
        public DateTime SecondStageEndDate { get; set; }



    }
}
