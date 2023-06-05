using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionBankController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IQuestionBankService _service;
        private readonly IService<InterviewQuestion> _questionService;
        private readonly IService<TechnicalConfiguration> _confService;


        public QuestionBankController(IMapper mapper, IQuestionBankService service, IService<InterviewQuestion> questionService, IService<TechnicalConfiguration> confService) //constructor
        {
            _mapper = mapper;
            _service = service;
            _questionService = questionService;
            _confService = confService;
        }


        [HttpGet("[action]/{interviewQuestionsId}")] //based on the id of the question bank get all the interview questions
        public async Task<IActionResult> GetSingleQuestionBankByIdWithInterviewQuestionsAsync(int interviewQuestionsId)
        {
            var q = await _service.GetSingleQuestionBankByIdWithInterviewQuestionsAsync(interviewQuestionsId);
            var conf = await _confService.Where(c => c.QuestionBankId == interviewQuestionsId).FirstOrDefaultAsync();
            q.QuestionTime = (float)conf.QuestionTime;

            return CreateActionResult(CustomResponseDto<QuestionBankWithInterviewQuestionDto>.Success(201, q)); 

        }


        [HttpGet("[action]")]  //get all question banks with interview questions
        public async Task<IActionResult> GetQuestionBanksWithInterviewQuestions()
        {

            return CreateActionResult(await _service.GetQuestionBanksWithInterviewQuestions());
        }


        //GET api/questionbanks
        [HttpGet] //Simple get
        public async Task<IActionResult> All()
        {
            var questionBanks = await _service.GetAllAsync();
            var questionBankDtos = _mapper.Map<List<QuestionBankDto>>(questionBanks.ToList());
            return CreateActionResult(CustomResponseDto<List<QuestionBankDto>>.Success(200, questionBankDtos));
        }

        //GET api/questionbank/5
        [HttpGet("{id}")] //Simple get with id
        public async Task<IActionResult> GetById(int id)
        {
            var questionBank = await _service.GetByIdAsync(id);
            //mapping
            var questionBankDtos = _mapper.Map<QuestionBankDto>(questionBank);
            return CreateActionResult(CustomResponseDto<QuestionBankDto>.Success(200, questionBankDtos));
        }

        [HttpPost] //Simple post
        public async Task<IActionResult> Save(QuestionBankDto questionBankDto)
        {
            var interviewQuestion = await _service.AddAsync(_mapper.Map<QuestionBank>(questionBankDto));
            //mapping
            var questionBankDtos = _mapper.Map<QuestionBankDto>(interviewQuestion);
            return CreateActionResult(CustomResponseDto<QuestionBankDto>.Success(201, questionBankDtos)); //201 = created
        }

        [HttpPut] //Simple put
        public async Task<IActionResult> Update(QuestionBankDto questionBankDto)
        {
            await _service.UpdateAsync(_mapper.Map<QuestionBank>(questionBankDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //204 = NoContent
        }

        // DELETE api/questionbank/5
        [HttpDelete("{id}")] //Simple delete
        public async Task<IActionResult> Remove(int id)
        {
            var questionBank = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(questionBank);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); 
        }


    }
}
