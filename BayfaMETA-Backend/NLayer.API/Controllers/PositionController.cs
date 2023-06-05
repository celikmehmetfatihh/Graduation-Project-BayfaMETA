using Autofac.Core;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Services;
using NPOI.SS.Formula.Functions;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : CustomBaseController
    {
        private readonly IPositionService _positionService;
        private readonly IMapper _mapper;
        private readonly IService<UserPosition> _userPositionService;
        private readonly IService<User> _userService;
        private readonly IService<InterviewQuestion> _interviewQuestionService;
        private readonly IService<QuestionBank> _questionBankService;
        private readonly IService<StageConfiguration> _stageConfigurationService;
        private readonly IService<ResumeConfiguration> _resumeConfigurationService;
        private readonly IService<TechnicalConfiguration> _technicalConfigurationService;
        private readonly IService<VideoInterviewConfiguration> _videoInterviewConfigurationService;
        private readonly IQuestionBankService _questionsBankService;
        private readonly IService<TechnicalConfiguration> _techConfigurationService;


        public PositionController(IPositionService positionService, IMapper mapper, IService<UserPosition> userPositionService, IService<User> userService, IService<InterviewQuestion> interviewQuestionService, IService<QuestionBank> questionBankService, IService<StageConfiguration> stageConfigurationService, IService<ResumeConfiguration> resumeConfigurationService, IService<TechnicalConfiguration> technicalConfigurationService, IService<VideoInterviewConfiguration> videoInterviewConfigurationService, IQuestionBankService questionsBankService, IService<TechnicalConfiguration> techConfigurationService)
        {
            _positionService = positionService;
            _mapper = mapper;
            _userPositionService = userPositionService;
            _userService = userService;
            _interviewQuestionService = interviewQuestionService;
            _questionBankService = questionBankService;
            _stageConfigurationService = stageConfigurationService;
            _resumeConfigurationService = resumeConfigurationService;
            _technicalConfigurationService = technicalConfigurationService;
            _videoInterviewConfigurationService = videoInterviewConfigurationService;
            _questionsBankService = questionsBankService;
            _techConfigurationService = techConfigurationService;
        }

        /// <summary>
        /// Get single position with its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var position = await _positionService.GetByIdAsync(id);
            //mapping
            var positionDtos = _mapper.Map<PositionDto>(position);
            return CreateActionResult(CustomResponseDto<PositionDto>.Success(200, positionDtos));
        }

        /// <summary>
        /// Add a position to the database
        /// </summary>
        /// <param name="positionDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save(PositionPostingDto positionDto)
        {
            positionDto.IsAvailable = true;
            positionDto.IsClosed = false;
            int bankId;
            try
            {
                Application excel = new Application();
                Workbook wb = excel.Workbooks.Open(positionDto.ExcelPath); //open our workbook store it in wb
                Worksheet ws = wb.Worksheets[1]; //first worksheet of the workbook


                Object cell = ws.Cells[1, 1].Value;

                QuestionBank questionBank = new QuestionBank();
                var bank = await _questionBankService.AddAsync(questionBank);
                bankId = bank.Id;



                int j = 1;
                while (true)
                {
                    if (ws.Cells[j, 1].Value == null)
                    {   
                        wb.Close();
                        break;

                    }
                    
                    InterviewQuestion interviewQuestions = new InterviewQuestion();
                    interviewQuestions.Question = Convert.ToString(ws.Cells[j, 1].Value);
                    interviewQuestions.OptionA = Convert.ToString(ws.Cells[j, 2].Value);
                    interviewQuestions.OptionB = Convert.ToString(ws.Cells[j, 3].Value);
                    interviewQuestions.OptionC = Convert.ToString(ws.Cells[j, 4].Value);
                    interviewQuestions.OptionD = Convert.ToString(ws.Cells[j, 5].Value);
                    interviewQuestions.OptionE = Convert.ToString(ws.Cells[j, 6].Value);
                    interviewQuestions.Answer = Convert.ToString(ws.Cells[j, 7].Value);

                    interviewQuestions.QuestionBankId = bank.Id;
                    await _interviewQuestionService.AddAsync(interviewQuestions);
                    j++;

                }
            }
            catch
            {
                return BadRequest();
            }

         
            var pos = _mapper.Map<Position>(positionDto);
            pos.IsAvailable = true;
            var position = await _positionService.AddAsync(pos);
            var tech = _mapper.Map<TechnicalConfiguration>(positionDto);
            var resume = _mapper.Map<ResumeConfiguration>(positionDto);
            var video = _mapper.Map<VideoInterviewConfiguration>(positionDto);
            tech.PositionId = position.Id;
            tech.QuestionBankId = bankId;
            resume.PositionId = position.Id;
            video.PositionId = position.Id;

            
            var a = await _technicalConfigurationService.AddAsync(tech);
            var b = await _resumeConfigurationService.AddAsync(resume);
            var c = await _videoInterviewConfigurationService.AddAsync(video);


            //mapping
            var positionDtos = _mapper.Map<PositionDto>(position);
            return CreateActionResult(CustomResponseDto<PositionDto>.Success(201, positionDtos)); 
        }

        /// <summary>
        /// Update a position in the database.
        /// </summary>
        /// <param name="positionDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(PositionDto positionDto)
        {
            await _positionService.UpdateAsync(_mapper.Map<Position>(positionDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); 
        }


        /// <summary>
        /// Delete a position from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var position = await _positionService.GetByIdAsync(id);
            await _positionService.RemoveAsync(position);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); 
        }

        /// <summary>
        /// Get all positions with their relevant user details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetPositionsWithUsersAsync(int id)
        {
            var userPositions = _userPositionService.Where(x => x.PositionId == id);

            var userPositionDto = _mapper.Map<List<UserPositionDto>>(userPositions.ToList());

            List<UserDto> users = new List<UserDto>();

            foreach(var position in userPositionDto)
            {
                User user = await _userService.GetByIdAsync(position.UserId);
                UserDto userDto = _mapper.Map<UserDto>(user);
                users.Add(userDto);
            }
            

            return CreateActionResult(CustomResponseDto<List<UserDto>>.Success(200, users));
        }

        /// <summary>
        /// Get all positions from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllPositions")]
        public async Task<IActionResult> All()
        {

            var positions = await _positionService.GetAllAsync();
            var positionDtos = _mapper.Map<List<PositionDto>>(positions.ToList());
            return CreateActionResult(CustomResponseDto<List<PositionDto>>.Success(200, positionDtos));
        }

        [HttpGet("GetAllAvailablePositions")]
        public async Task<IActionResult> GetAvailable()
        {

            var positions =  _positionService.Where(p => p.IsAvailable).ToList();
            var positionDtos = _mapper.Map<List<PositionDto>>(positions.ToList());
            return CreateActionResult(CustomResponseDto<List<PositionDto>>.Success(200, positionDtos));
        }


        [HttpGet("[action]/{positionId}")]
        public async Task<IActionResult> GetVideoInterviewConfiguration(int positionId)
        {
            var conf = _videoInterviewConfigurationService.Where(c => c.PositionId == positionId).SingleOrDefault();
            var dto = _mapper.Map<VideoInterviewConfigurationDto>(conf);
            return CreateActionResult(CustomResponseDto<VideoInterviewConfigurationDto>.Success(200, dto));
        }

        [HttpGet("[action]/{positionId}")]
        public async Task<IActionResult> GetResumeConfiguration(int positionId)
        {
            var conf = _resumeConfigurationService.Where(c => c.PositionId == positionId).SingleOrDefault();
            var dto = _mapper.Map<ResumeConfigurationDto>(conf);
            return CreateActionResult(CustomResponseDto<ResumeConfigurationDto>.Success(200, dto));
        }

        [HttpGet("[action]/{positionId}")]
        public async Task<IActionResult> GetTechnicalQuestionsOfPosition(int positionId)
        {
            var position = await _positionService.GetByIdAsync(positionId);
            var conf = _techConfigurationService.Where(t => t.PositionId == position.Id).FirstOrDefault();


            var questions =
                await _questionsBankService.GetSingleQuestionBankByIdWithInterviewQuestionsAsync(conf.QuestionBankId);
            questions.QuestionTime =(float) conf.QuestionTime;


            return CreateActionResult(CustomResponseDto<QuestionBankWithInterviewQuestionDto>.Success(201, questions));
        }





    }
}
