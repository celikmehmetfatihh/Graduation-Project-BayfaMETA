using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Repositoryy;
using RestSharp;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StageAndTotalScoreController : CustomBaseController
    {
        private readonly IService<Position> _positionService;
        private readonly IService<VideoInterviewScore> _videoScoreService;
        private readonly IService<TechnicalScore> _technicalScoreService;
        private readonly IService<ResumeScore> _resumeScoreService;
        private readonly IService<Resume> _resumeService;
        private readonly IService<VideoInterview> _videoInterviewService;
        private readonly AppDbContext _appDbContext;
        private readonly IService<UserPosition> _userPositionService;
        private readonly IService<User> _userService;


        public StageAndTotalScoreController(IService<Position> positionService, IService<VideoInterviewScore> videoScoreService, IService<TechnicalScore> technicalScoreService, IService<ResumeScore> resumeScoreService, AppDbContext appDbContext, IService<Resume> resumeService, IService<VideoInterview> videoInterviewService, IService<UserPosition> userPositionService, IService<User> userService)
        {
            _positionService = positionService;
            _videoScoreService = videoScoreService;
            _technicalScoreService = technicalScoreService;
            _resumeScoreService = resumeScoreService;
            _appDbContext = appDbContext;
            _resumeService = resumeService;
            _videoInterviewService = videoInterviewService;
            _userPositionService = userPositionService;
            _userService = userService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Calculate()
        {
            List<string> emails;
            List<string> failedEmails = new List<string>();
            List<string> passedEmails = new List<string>();
            var positions =  _appDbContext.Positions.Include(p=>p.userPositions).ToList();
            

            
            foreach (var position in positions)
            {
                if (position.FirstStageEndDate < DateTime.Now && position.IsAvailable)
                {
                    position.IsAvailable = false;
                    await _positionService.UpdateAsync(position);
                    foreach (var up in position.userPositions)
                    {
                        var resume =
                            _resumeService.Where(r => r.PositionId == up.PositionId && r.UserId == up.UserId).FirstOrDefault();
                        var resumeScore = _resumeScoreService.Where(rs => rs.ResumeId == resume.Id).FirstOrDefault();
                        if (resumeScore?.TotalScore >= position.StageOneThreshold)
                        {
                            up.isFirstStagePassed = true;
                            var user = await _userService.GetByIdAsync(up.UserId);
                            passedEmails.Add(user.email);
                        }
                        else
                        {
                            up.isFirstStagePassed = false;
                            var user = await _userService.GetByIdAsync(up.UserId);
                            failedEmails.Add(user.email);
                        }

                        await _userPositionService.UpdateAsync(up);
                    }
                    string URL = "http://127.0.0.1:5000/";
                    var client = new RestClient(URL);
                    var req = new RestRequest("/PassedEmails", Method.Post);

                    req.RequestFormat = DataFormat.Json;
                    emails = passedEmails;
                    req.AddBody(emails);
                    var response = await client.ExecuteAsync(req);


                    client = new RestClient(URL);
                    req = new RestRequest("/FailedEmails", Method.Post);

                    req.RequestFormat = DataFormat.Json;
                    emails = failedEmails;
                    req.AddBody(emails);
                    response = await client.ExecuteAsync(req);

                }

                else if (position.SecondStageEndDate < DateTime.Now && (bool)!position.IsClosed)
                {
                    foreach (var up in position.userPositions)
                    {
                        if ((bool)up.isFirstStagePassed)
                        {
                            var resume =
                                _resumeService.Where(r => r.PositionId == up.PositionId && r.UserId == up.UserId).FirstOrDefault();
                            var resumeScore = _resumeScoreService.Where(rs => rs.ResumeId == resume.Id).FirstOrDefault();

                            
                            var technicalScore = _technicalScoreService.Where(t => t.PositionId == up.PositionId && t.UserId == up.UserId).FirstOrDefault();

                            var video =
                                _videoInterviewService.Where(v => v.PositionId == up.PositionId && v.UserId == up.UserId).FirstOrDefault();
                            var videoScore = _videoScoreService.Where(vs => vs.VideoInterviewId == video.Id).FirstOrDefault();

                            float total = (float)((position.TechnicalTestMultiplier * technicalScore.Score / 100)
                                          + (position.VideoInterviewMultiplier * videoScore.score / 100)
                                          + (position.ResumeMultiplier * resumeScore.TotalScore / 100));

                            up.FinalScore = total;
                            up.isSecondStageFinished = true;
                            position.IsClosed = true;
                            
                            await _positionService.UpdateAsync(position);
                            await _userPositionService.UpdateAsync(up);

                        }


                        
                    }
                }
            }

            return Ok();
        }
    }
}
