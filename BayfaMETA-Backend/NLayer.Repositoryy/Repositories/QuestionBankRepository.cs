using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repositoryy.Repositories
{
    public class QuestionBankRepository : GenericRepository<QuestionBank>, IQuestionBankRepository
    {
        public QuestionBankRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<QuestionBank>> GetQuestionBanksWithInterviewQuestions()
        {
           
            return await _context.QuestionBank.Include(x => x.InterviewQuestions).ToListAsync();

        }

        public async Task<QuestionBank> GetSingleQuestionBankByIdWithInterviewQuestionsAsync(int interviewQuestionsId)
        {
            return await _context.QuestionBank.Include(x => x.InterviewQuestions).Where(x => x.Id == interviewQuestionsId).SingleOrDefaultAsync();
        }
    }
}
