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
    public class TechnicalConfigurationController : CustomBaseController
    {
        private readonly IService<TechnicalConfiguration> _service;
        private readonly IMapper _mapper;

        public TechnicalConfigurationController(IService<TechnicalConfiguration> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var technicalConfiguration = await _service.GetByIdAsync(id);
            //mapping
            var stageConfigurationDtos = _mapper.Map<TechnicalConfigurationDto>(technicalConfiguration);
            return CreateActionResult(CustomResponseDto<TechnicalConfigurationDto>.Success(200, stageConfigurationDtos));
        }

        [HttpPost]
        public async Task<IActionResult> Save(TechnicalConfigurationDto technicalConfigurationDto)
        {
            var technicalConfiguration = await _service.AddAsync(_mapper.Map<TechnicalConfiguration>(technicalConfigurationDto));
            //mapping
            var technicalConfigurationDtos = _mapper.Map<TechnicalConfigurationDto>(technicalConfiguration);
            return CreateActionResult(CustomResponseDto<TechnicalConfigurationDto>.Success(201, technicalConfigurationDtos)); //201 = created
        }

        [HttpPut]
        public async Task<IActionResult> Update(TechnicalConfigurationDto technicalConfigurationDto)
        {
            await _service.UpdateAsync(_mapper.Map<TechnicalConfiguration>(technicalConfigurationDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //204 = NoContent
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var technicalConfiguration = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(technicalConfiguration);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); //Geriye bir şey dönülmüyor
        }
    }
}
