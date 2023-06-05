using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    /// <summary>
    /// Class for storing video interview details of a user.
    /// </summary>
    public class VideoInterview:Base
    {
        public DateTime? date { get; set; }
        public int? size { get; set; }
        public String? format { get; set; }
        public int UserId { get; set; } 
        public User User { get; set; }
        public VideoInterviewScore VideoInterviewScore { get; set; }
        public String? path { get; set; }
        public int? PositionId { get; set; }
        public Position Position { get; set; }
    }
}
