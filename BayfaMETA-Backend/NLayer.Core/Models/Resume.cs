using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    /// <summary>
    /// Class for storing resume information of an applicant
    /// </summary>
    public class Resume : Base
    {
        public int PositionId { get; set; }
        public Position? Position { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
