using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    /// <summary>
    /// Class for storing user details.
    /// </summary>
    public class User:Base
    {
        public String email { get; set; }
        public String password { get; set; }
        public String? userName { get; set; }
        public String? name { get; set; }
        public String? surname { get; set; }
        public String? tel_no { get; set; }
        public String? role { get; set; }
        public ICollection<VideoInterview> videoInterviews { get; set; } 
        public ICollection<UserPosition> userPositions { get; set; }
    }
}
