using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class QuestionBank
    {
        public int Id { get; set; }
        public ICollection<InterviewQuestion> ?InterviewQuestions { get; set; }
    }
}
