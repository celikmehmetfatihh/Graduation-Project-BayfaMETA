using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    /// <summary>
    /// Class for configuring technical question part.
    /// </summary>
    public class TechnicalConfiguration : Base
    {
        public Position? Position { get; set; }
        public int? PositionId { get; set; }

        public QuestionBank? QuestionBank { get; set; }
        public int QuestionBankId { get; set; }
        public string? ExcelPath { get; set; }
        public int? QuestionTime { get; set; }

    }
}
