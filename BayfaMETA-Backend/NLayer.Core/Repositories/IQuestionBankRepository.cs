using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    public interface IQuestionBankRepository:IGenericRepository<QuestionBank>
    {
        Task<List<QuestionBank>> GetQuestionBanksWithInterviewQuestions();

        Task<QuestionBank> GetSingleQuestionBankByIdWithInterviewQuestionsAsync(int interviewQuestionsId);
    }
}
