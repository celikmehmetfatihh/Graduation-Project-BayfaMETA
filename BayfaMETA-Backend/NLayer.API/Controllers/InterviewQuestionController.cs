using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json.Linq;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NPOI.SS.UserModel;
using RestSharp;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewQuestionController : CustomBaseController

    {
        private readonly IMapper _mapper;
        private readonly IService<InterviewQuestion> _service;
        private readonly IService<QuestionBank> _serviceBank;


        public InterviewQuestionController(IService<InterviewQuestion> service, IMapper mapper, IService<QuestionBank> serviceBank)
        {
            _service = service;
            _mapper = mapper;
            _serviceBank = serviceBank;
        }

        /// <summary>
        /// Simple get for interview questions
        /// </summary>
        /// <returns></returns>
        //GET api/scores
        [HttpGet]
        public async Task<IActionResult> All()
        {

            var interviewQuestions = await _service.GetAllAsync();
            //mapping
            var interviewQuestionDtos = _mapper.Map<List<InterviewQuestionDto>>(interviewQuestions.ToList());
            return CreateActionResult(CustomResponseDto<List<InterviewQuestionDto>>.Success(200, interviewQuestionDtos));
        }
        /// <summary>
        /// Simple get for interview questions according to their ids
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/score/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var interviewQuestion = await _service.GetByIdAsync(id);
            //mapping
            var interviewQuestionDtos = _mapper.Map<InterviewQuestionDto>(interviewQuestion);
            return CreateActionResult(CustomResponseDto<InterviewQuestionDto>.Success(200, interviewQuestionDtos));
        }

        /// <summary>
        /// Simple post for interview questions
        /// </summary>
        /// <param name="interviewQuestionDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save(InterviewQuestionDto interviewQuestionDto)
        {
            var interviewQuestion = await _service.AddAsync(_mapper.Map<InterviewQuestion>(interviewQuestionDto));
            //mapping
            var interviewQuestionDtos = _mapper.Map<InterviewQuestionDto>(interviewQuestion);
            return CreateActionResult(CustomResponseDto<InterviewQuestionDto>.Success(201, interviewQuestionDtos)); //201 = created
        }

        /// <summary>
        /// Simple put for interview questions
        /// </summary>
        /// <param name="interviewQuestionDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(InterviewQuestionDto interviewQuestionDto)
        {
            await _service.UpdateAsync(_mapper.Map<InterviewQuestion>(interviewQuestionDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //204 = NoContent
        }

        /// <summary>
        /// Simple delete for interview questions
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/scores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var interviewQuestion = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(interviewQuestion);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //Geriye bir şey dönülmüyor
        }

        /// <summary>
        /// First this endpoint gets a string named filePath, then using this filepath it opens the workbook and worksheet of that Microsoft Excel.
        /// Then, till the program reads a null value, interview question object will be created, filled with the data from excel, and send it to the database.
        /// If we catch some errors BadRequest will be displayed.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string filePath)
        {

            try
            {
                Application excel = new Application();
                Workbook wb = excel.Workbooks.Open(filePath); //open our workbook store it in wb
                Worksheet ws = wb.Worksheets[1]; //first worksheet of the workbook


                Object cell = ws.Cells[1, 1].Value;

                QuestionBank questionBank = new QuestionBank();
                var bank = await _serviceBank.AddAsync(questionBank); //database'e questinbank eklemek



                int j = 1;
                while (true)
                {
                    if (ws.Cells[j, 1].Value == null)
                        break;
                    else
                    {

                        InterviewQuestion interviewQuestions = new InterviewQuestion();
                        interviewQuestions.Question = Convert.ToString(ws.Cells[j, 1].Value);
                        interviewQuestions.OptionA = Convert.ToString(ws.Cells[j, 2].Value);
                        interviewQuestions.OptionB = Convert.ToString(ws.Cells[j, 3].Value);
                        interviewQuestions.OptionC = Convert.ToString(ws.Cells[j, 4].Value);
                        interviewQuestions.OptionD = Convert.ToString(ws.Cells[j, 5].Value);
                        interviewQuestions.OptionE = Convert.ToString(ws.Cells[j, 6].Value);
                        interviewQuestions.Answer = Convert.ToString(ws.Cells[j, 7].Value);

                        interviewQuestions.QuestionBankId = bank.Id;
                        await _service.AddAsync(interviewQuestions);
                        j++;
                    }
                }

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
