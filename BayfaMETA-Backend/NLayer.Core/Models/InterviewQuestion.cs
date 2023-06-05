using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class InterviewQuestion
    {
        public int Id { get; set; } //primary key

        public int ?QuestionBankId { get; set; } //foreign key 
        public QuestionBank ?QuestionBank { get; set; }

        public string Question { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string OptionE { get; set; }
        public string Answer { get; set; }
    }
}
