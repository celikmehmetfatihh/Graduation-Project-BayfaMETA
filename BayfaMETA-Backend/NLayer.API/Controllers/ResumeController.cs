using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Services;


namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Resume> _resumeService;
        private readonly IService<ResumeScore> _resumeScoreService;
        private readonly IService<ResumeConfiguration> _configurationService;
        private readonly IService<UserPosition> _userPositionService;

        public ResumeController(IMapper mapper, IService<Resume> resumeService, IService<ResumeScore> resumeScoreService, IService<ResumeConfiguration> configurationService, IService<UserPosition> userPositionService)
        {
            _mapper = mapper;
            _resumeService = resumeService;
            _resumeScoreService = resumeScoreService;
            _configurationService = configurationService;
            _userPositionService = userPositionService;
        }

        /// <summary>
        /// Adding a resume with its details to the database.
        /// This endpoint will be used by python.
        /// Also resume score is added to the database independently
        /// by extracting resume dto to both ResumeScore and Resume objects.
        /// </summary>
        /// <param name="resumeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save(ResumeDto resumeDto)
        {
            var resume = await _resumeService.AddAsync(_mapper.Map<Resume>(resumeDto));
            var resumeDtos = _mapper.Map<ResumeDto>(resume);

            ResumeScore resumeScore =
                new ResumeScore()
                {
                    MatchingScore = resumeDto.MatchingScore,
                    TotalScore = resumeDto.TotalScore,
                    ResumeId = resumeDtos.Id
                };

            UserPosition userPosition =
                new UserPosition()
                {
                    isTechnicalQuestionCompleted = false,
                    isFirstStagePassed = false,
                    isSecondStageFinished = false,
                    isVideoInterviewCompleted = false,
                    PositionId = resume.PositionId,
                    UserId = resume.UserId
                };


            await _resumeScoreService.AddAsync(resumeScore);
            await _userPositionService.AddAsync(userPosition);
            

            return CreateActionResult(CustomResponseDto<ResumeDto>.Success(201, resumeDtos));
        }


        /// <summary>
        /// Endpoint for getting desired resume keywords for a specific position
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{positionId}")]
        public async Task<IActionResult> GetResumeKeywordsByPositionId(int positionId)
        {

            var conf = await _configurationService.GetAllAsync();
            var selectedConf = conf.Where(s => s.PositionId == positionId).SingleOrDefault();


            //mapping
            var confDto = _mapper.Map<ResumeConfigurationDto>(selectedConf);
            return CreateActionResult(CustomResponseDto<ResumeConfigurationDto>.Success(200, confDto));
        }


    }
}
