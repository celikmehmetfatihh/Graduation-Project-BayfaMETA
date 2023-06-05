using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Services;
using NReco.VideoConverter;
using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;
using System;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateFilterAttribute]
    public class VideoInterviewsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IVideoInterviewService _service;
        private readonly IService<VideoInterviewScore> _scoreService;
        private readonly IService<UserPosition> _userPositionService;

        public VideoInterviewsController(IMapper mapper, IVideoInterviewService interviewService, IService<VideoInterviewScore> score, IService<UserPosition> userPositionService)
        {
            _mapper = mapper;
            _service = interviewService;
            _scoreService = score;
            _userPositionService = userPositionService;
        }

        [HttpGet("[action]/{videoInterviewId}")]
        public async Task<IActionResult> GetSingleVideoInterviewByIdWithScoresAsync(int videoInterviewId)
        {
            var x = CreateActionResult(await _service.GetSingleVideoInterviewByIdWithScoresAsync(videoInterviewId));
            return x;

        }

        // GET api/products/GetVideoInterviewsWithUser
        [HttpGet("[action]")] //action methodun ismini direkt alır
        public async Task<IActionResult> GetVideoInterviewsWithUser()
        {

            return CreateActionResult(await _service.GetVideoInterviewsWithUser());
        }


  
        [HttpGet]
        public async Task<IActionResult> All()
        {
           
            var videoInterviews = await _service.GetAllAsync();
            //mapping
            var videoInterviewsDtos = _mapper.Map<List<VideoInterviewDto>>(videoInterviews.ToList());
            return CreateActionResult(CustomResponseDto<List<VideoInterviewDto>>.Success(200, videoInterviewsDtos));
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var videoInterview = await _service.GetByIdAsync(id);
            //mapping
            var videoInterviewsDtos = _mapper.Map<VideoInterviewDto>(videoInterview);
            return CreateActionResult(CustomResponseDto<VideoInterviewDto>.Success(200, videoInterviewsDtos));
        }

        [HttpPost]
        public async Task<IActionResult> Save(VideoInterviewDto videoInterviewDto)
        {
            var videoInterview = await _service.AddAsync(_mapper.Map<VideoInterview>(videoInterviewDto));
            //mapping
            var videoInterviewsDtos = _mapper.Map<VideoInterviewDto>(videoInterview);
            return CreateActionResult(CustomResponseDto<VideoInterviewDto>.Success(201, videoInterviewsDtos)); //201 = created
        }

        [HttpPut]
        public async Task<IActionResult> Update(VideoInterviewDto videoInterviewDto)
        {
            await _service.UpdateAsync(_mapper.Map<VideoInterview>(videoInterviewDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //204 = NoContent
        }

        // DELETE api/videointerviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var videoInterview = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(videoInterview);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //Geriye bir şey dönülmüyor
        }

        //Adding video interview and score with userId
        [HttpPost("upload/{userId}/{positionId}")]
        public async Task<IActionResult> Upload(int userId, int positionId)
        {
            
            try
            {
                var form = Request.Form;
                foreach (var myFile in form.Files)
                {
                    Random random = new Random();
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    string randomName = new string(Enumerable.Repeat(chars, 10)
                        .Select(s => s[random.Next(s.Length)]).ToArray());

                    var path = Path.Combine("C://video/", form.Files[0].FileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        myFile.CopyTo(fileStream);

                    }
                    var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
                    ffMpeg.ConvertMedia(path, $"C://video/{randomName}.mp4", Format.mp4);
                    Thread.Sleep(5000);
                    path = Path.Combine($"C://video/{randomName}.mp4");

                    VideoInterviewDto fileDto = new VideoInterviewDto
                    {
                        UserId = userId,
                        PositionId = positionId,
                        path = path,
                        format = "mp4",
                        date = DateTime.Now
                    };

                    var file = await _service.AddAsync(_mapper.Map<Core.Models.VideoInterview>(fileDto));
                    var filesDto = _mapper.Map<VideoInterviewDto>(file);

                    string URL = "http://127.0.0.1:5000/";
                    var client = new RestClient(URL);
                    var req = new RestRequest("InterviewScores?path=" + fileDto.path + "&positionId=" + positionId);
                    var response = await client.ExecuteAsync(req);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string rawResponse = response.Content;
                        dynamic obj = JValue.Parse(rawResponse);
                        float openness = obj.Openness;
                        float conscientiousness = obj.Conscientiousness;
                        float extraversion = obj.Extraversion;
                        float agreeableness = obj.Agreeableness;
                        float neuroticism = obj.Neuroticism;
                        float totalScore = obj.InterviewScore;

                        VideoInterviewScoreDto score = new VideoInterviewScoreDto
                        {
                            openness = openness,
                            conscientiousness = conscientiousness,
                            extraversion = extraversion,
                            agreeableness = agreeableness,
                            neuroticism = neuroticism,
                            VideoInterviewId = filesDto.Id,
                            score = totalScore,
                            score_type = "video_score"

                        };

                        var temp = await _scoreService.AddAsync(_mapper.Map<Core.Models.VideoInterviewScore>(score));
                        var scoresDto = _mapper.Map<VideoInterviewScoreDto>(temp);
                    }
                }

                var userPosition = _userPositionService.Where(up => up.PositionId == positionId && up.UserId == userId)
                    .FirstOrDefault();
                userPosition.isVideoInterviewCompleted = true;
                await _userPositionService.UpdateAsync(userPosition);



                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        private float LimitDecimalPlace(float number, int limitPlace)
        {
            float result = 0;
            string sNumber = number.ToString();
            int decimalIndex = sNumber.IndexOf(".");
            if (decimalIndex != -1)
            {
                sNumber = sNumber.Remove(decimalIndex + limitPlace + 1);
            }

            result = float.Parse(sNumber);
            return result;
        }


    }
}


