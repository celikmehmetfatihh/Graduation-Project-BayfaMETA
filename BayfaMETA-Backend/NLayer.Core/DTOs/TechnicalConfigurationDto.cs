using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class TechnicalConfigurationDto : BaseDto
    {
        public int QuestionBankId { get; set; }
        public int PositionId { get; set; }
        public int QuestionTime { get; set; }

    }
}
