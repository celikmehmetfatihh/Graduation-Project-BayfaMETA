using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class UserCompletionDto
    {
        public int UserId { get; set; }
        public int PositionId { get; set; }
        public bool? isTechnicalQuestionCompleted { get; set; }
        public bool? isVideoInterviewCompleted { get; set; }
        public bool? isFirstStagePassed { get; set; }
        public bool? isSecondStageFinished { get; set; }
        public float FinalScore { get; set; }
    }
}
