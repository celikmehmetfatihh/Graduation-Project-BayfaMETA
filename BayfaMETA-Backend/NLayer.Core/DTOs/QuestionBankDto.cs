using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Core.Models;

namespace NLayer.Core.DTOs
{
    public class QuestionBankDto:BaseDto
    {
        public ICollection<InterviewQuestion> ?InterviewQuestions { get; set; }
    }
}
