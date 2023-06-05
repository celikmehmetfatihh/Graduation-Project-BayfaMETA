using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    /// <summary>
    /// As user and position have many to many relationship
    /// their relation must be kept in a seperate table,
    /// so UserPosition model, table is created.
    /// It also stores stage completion details of a single user
    /// considering a specific position.
    /// </summary>
    public class UserPosition
    {
        public int UserId { get; set; }
        public int PositionId { get; set; }
        public User User { get; set; }
        public Position Position { get; set; }
        public bool? isTechnicalQuestionCompleted { get; set; }
        public bool? isVideoInterviewCompleted { get; set; }
        public bool? isFirstStagePassed { get; set; }
        public bool? isSecondStageFinished { get; set; }
        public float FinalScore { get; set; }
    }
}
