using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class PositionService : Service<Position>, IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public PositionService(IPositionRepository positionRepository, IGenericRepository<Position> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;

        }

        public async Task<CustomResponseDto<PositionWithUsersDto>> GetAllPositionsWithUsers(int positionId)
        {
            var position = await _positionRepository.GetAllPositionsWithUsers(positionId);
            var positionDto = _mapper.Map<PositionWithUsersDto>(position);

            return CustomResponseDto<PositionWithUsersDto>.Success(200, positionDto);
        }
    }
}
