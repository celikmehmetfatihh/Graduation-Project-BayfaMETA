using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class QuestionBankWithInterviewQuestionDto: QuestionBankDto
    {
        public List<InterviewQuestionDto> InterviewQuestions { get; set; }
        public float QuestionTime { get; set; }

    }   
}
