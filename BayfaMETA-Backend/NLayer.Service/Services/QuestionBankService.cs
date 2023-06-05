using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repositoryy.Repositories;

namespace NLayer.Service.Services
{
    public class QuestionBankService : Service<QuestionBank>, IQuestionBankService
    {
        private readonly IQuestionBankRepository _questionBankRepository;
        private readonly IMapper _mapper;
        public QuestionBankService(IGenericRepository<QuestionBank> repository, IUnitOfWork unitOfWork, IQuestionBankRepository questionBankRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _questionBankRepository = questionBankRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<QuestionBankWithInterviewQuestionDto>>> GetQuestionBanksWithInterviewQuestions()
        {
            var questionBanks = await _questionBankRepository.GetQuestionBanksWithInterviewQuestions();

            var questionBanksDto = _mapper.Map<List<QuestionBankWithInterviewQuestionDto>>(questionBanks);

            return CustomResponseDto<List<QuestionBankWithInterviewQuestionDto>>.Success(200, questionBanksDto);
        }

        public async Task<QuestionBankWithInterviewQuestionDto> GetSingleQuestionBankByIdWithInterviewQuestionsAsync(int interviewQuestionsId)
        {
            var interviewQuestion = await _questionBankRepository.GetSingleQuestionBankByIdWithInterviewQuestionsAsync(interviewQuestionsId);

            var questionBankDto = _mapper.Map<QuestionBankWithInterviewQuestionDto>(interviewQuestion);

            return  questionBankDto;
        }
    }
}
