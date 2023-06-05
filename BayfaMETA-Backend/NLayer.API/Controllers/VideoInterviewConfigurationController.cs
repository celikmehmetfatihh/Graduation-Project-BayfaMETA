using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoInterviewConfigurationController : CustomBaseController
    {
        private readonly IService<VideoInterviewConfiguration> _service;
        private readonly IMapper _mapper;

        public VideoInterviewConfigurationController(IService<VideoInterviewConfiguration> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var videoInterviewConfiguration = await _service.GetByIdAsync(id);
            //mapping
            var stageConfigurationDtos = _mapper.Map<VideoInterviewConfigurationDto>(videoInterviewConfiguration);
            return CreateActionResult(CustomResponseDto<VideoInterviewConfigurationDto>.Success(200, stageConfigurationDtos));
        }

        [HttpPost]
        public async Task<IActionResult> Save(VideoInterviewConfigurationDto videoInterviewConfigurationDto)
        {
            var videoInterviewConfiguration = await _service.AddAsync(_mapper.Map<VideoInterviewConfiguration>(videoInterviewConfigurationDto));
            //mapping
            var stageConfigurationDtos = _mapper.Map<VideoInterviewConfigurationDto>(videoInterviewConfiguration);
            return CreateActionResult(CustomResponseDto<VideoInterviewConfigurationDto>.Success(201, stageConfigurationDtos)); //201 = created
        }

        [HttpPut]
        public async Task<IActionResult> Update(VideoInterviewConfigurationDto videoInterviewConfigurationDto)
        {
            await _service.UpdateAsync(_mapper.Map<VideoInterviewConfiguration>(videoInterviewConfigurationDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //204 = NoContent
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var videoInterviewConfiguration = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(videoInterviewConfiguration);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //Geriye bir şey dönülmüyor
        }
    }
}
