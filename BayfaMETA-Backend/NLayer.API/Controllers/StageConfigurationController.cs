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
    public class StageConfigurationController : CustomBaseController
    {
        private readonly IService<StageConfiguration> _service;
        private readonly IMapper _mapper;

        public StageConfigurationController(IService<StageConfiguration> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stageConfiguration = await _service.GetByIdAsync(id);
            //mapping
            var stageConfigurationDtos = _mapper.Map<StageConfigurationDto>(stageConfiguration);
            return CreateActionResult(CustomResponseDto<StageConfigurationDto>.Success(200, stageConfigurationDtos));
        }

        [HttpPost]
        public async Task<IActionResult> Save(StageConfigurationDto stageConfigurationDto)
        {
            var stageConfiguration = await _service.AddAsync(_mapper.Map<StageConfiguration>(stageConfigurationDto));
            //mapping
            var stageConfigurationDtos = _mapper.Map<StageConfigurationDto>(stageConfiguration);
            return CreateActionResult(CustomResponseDto<StageConfigurationDto>.Success(201, stageConfigurationDtos)); //201 = created
        }

        [HttpPut]
        public async Task<IActionResult> Update(StageConfigurationDto stageConfigurationDto)
        {
            await _service.UpdateAsync(_mapper.Map<StageConfiguration>(stageConfigurationDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //204 = NoContent
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var stageConfiguration = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(stageConfiguration);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //Geriye bir şey dönülmüyor
        }
    }
}
