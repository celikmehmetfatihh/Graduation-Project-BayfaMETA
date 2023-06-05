using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IQuestionBankService:IService<QuestionBank>
    {
        public Task<CustomResponseDto<List<QuestionBankWithInterviewQuestionDto>>> GetQuestionBanksWithInterviewQuestions();

        public Task<QuestionBankWithInterviewQuestionDto> GetSingleQuestionBankByIdWithInterviewQuestionsAsync(int interviewQuestionsId);
    }
}
