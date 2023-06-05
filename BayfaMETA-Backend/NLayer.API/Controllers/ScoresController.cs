using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Repositoryy;
using NPOI.SS.Formula.Functions;
using RestSharp;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [ValidateFilterAttribute]
    public class ScoresController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<VideoInterviewScore> _service;
        private readonly IService<TechnicalScore> _technicalScoreService;
        private readonly IService<UserPosition> _userPositionService;
        private readonly IService<Position> _positionService;
        private readonly IService<VideoInterviewScore> _videoScoreService;
        private readonly IService<ResumeScore> _resumeScoreService;
        private readonly IService<Resume> _resumeService;
        private readonly IService<VideoInterview> _videoInterviewService;
        private readonly IService<User> _userService;
        private readonly AppDbContext _appDbContext;



        public ScoresController(IService<VideoInterviewScore> service, IMapper mapper, IService<TechnicalScore> technicalScoreService, IService<UserPosition> userPositionService, IService<Position> positionService, IService<VideoInterviewScore> videoScoreService, IService<ResumeScore> resumeScoreService, IService<Resume> resumeService, IService<VideoInterview> videoInterviewService, AppDbContext appDbContext, IService<User> userService)
        {
            _service = service;
            _mapper = mapper;
            _technicalScoreService = technicalScoreService;
            _userPositionService = userPositionService;
            _positionService = positionService;
            _videoScoreService = videoScoreService;
            _resumeScoreService = resumeScoreService;
            _resumeService = resumeService;
            _videoInterviewService = videoInterviewService;
            _appDbContext = appDbContext;
            _userService = userService;
        }





        //GET api/scores
        [HttpGet]
        public async Task<IActionResult> All()
        {

            var scores = await _service.GetAllAsync();
            //mapping
            var scoresDtos = _mapper.Map<List<VideoInterviewScoreDto>>(scores.ToList());
            return CreateActionResult(CustomResponseDto<List<VideoInterviewScoreDto>>.Success(200, scoresDtos));
        }


        //GET api/score/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var score = await _service.GetByIdAsync(id);
            //mapping
            var scoresDtos = _mapper.Map<VideoInterviewScoreDto>(score);
            return CreateActionResult(CustomResponseDto<VideoInterviewScoreDto>.Success(200, scoresDtos));
        }

        [HttpPost]
        public async Task<IActionResult> Save(VideoInterviewScoreDto scoreDto)
        {
            var score = await _service.AddAsync(_mapper.Map<VideoInterviewScore>(scoreDto));
            //mapping
            var scoresDtos = _mapper.Map<VideoInterviewScoreDto>(score);
            return CreateActionResult(CustomResponseDto<VideoInterviewScoreDto>.Success(201, scoresDtos)); 
        }

        [HttpPut]
        public async Task<IActionResult> Update(VideoInterviewScoreDto scoreDto)
        {
            await _service.UpdateAsync(_mapper.Map<VideoInterviewScore>(scoreDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //204 = NoContent
        }

        // DELETE api/scores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var score = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(score);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); 
        }


        [HttpGet("[action]/{userId}/{positionId}/{score}")]
        public async Task<IActionResult> AddTechnicalScore(int userId, int positionId, float score)
        {
            var technicalScore = new TechnicalScore()
            {
                UserId = userId,
                Score = score,
                PositionId = positionId
            };


            await _technicalScoreService.AddAsync(technicalScore);
            var userPosition = _userPositionService.Where(up => up.PositionId == positionId && up.UserId == userId).FirstOrDefault();
            userPosition.isTechnicalQuestionCompleted = true;
            await _userPositionService.UpdateAsync(userPosition);
            return Ok();
        }




        [HttpGet("GetOverallScores/{positionId}")]
        public async Task<IActionResult> GetOverallScores(int positionId)
        {
            List<OverallScoresDto> overallScores = new();
            var userPositions = _userPositionService.Where(u => u.PositionId == positionId).ToList();

            foreach (var up in userPositions)
            {
                if ((bool)up.isSecondStageFinished && (bool)up.isFirstStagePassed)
                {
                    var resume =
                        _resumeService.Where(r => r.PositionId == up.PositionId && r.UserId == up.UserId).FirstOrDefault();
                    var resumeScore = _resumeScoreService.Where(rs => rs.ResumeId == resume.Id).FirstOrDefault();

                    var technicalScore = _technicalScoreService.Where(t => t.PositionId == up.PositionId && t.UserId == up.UserId).FirstOrDefault();

                    var video =
                        _videoInterviewService.Where(v => v.PositionId == up.PositionId && v.UserId == up.UserId).FirstOrDefault();
                    var videoScore = _videoScoreService.Where(vs => vs.VideoInterviewId == video.Id).FirstOrDefault();

                    var user = await _userService.GetByIdAsync(up.UserId);

                    OverallScoresDto scores = new();
                    scores.TechnicalScore = technicalScore.Score;
                    scores.VideoScore = videoScore.score;
                    scores.ResumeMatching = (float)resumeScore.MatchingScore;
                    scores.ResumeTotalScore = (float)resumeScore.TotalScore;
                    scores.OverallScore = up.FinalScore;
                    scores.FullName = user.name + " " + user.surname;
                    scores.Email = user.email;
                    scores.TelNo = user.tel_no;
                    
                    overallScores.Add(scores);


                }
                

            }
            


            return CreateActionResult(CustomResponseDto<List<OverallScoresDto>>.Success(201, overallScores));
        }




    }







}
