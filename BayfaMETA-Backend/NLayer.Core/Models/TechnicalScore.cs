using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class TechnicalScore : Base
    {
        public float Score { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
    }
}
