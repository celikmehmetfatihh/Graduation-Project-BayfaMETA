using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repositoryy.Repositories;

namespace NLayer.Service.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<UserWithVideoInterviewsDto>> GetSingleUserByIdWithVideoInterviewsAsync(int userId)
        {
            var user = await _userRepository.GetSingleUserByIdWithVideoInterviewsAsync(userId);

            var userDto = _mapper.Map<UserWithVideoInterviewsDto>(user);

            return CustomResponseDto<UserWithVideoInterviewsDto>.Success(200, userDto);
        }

        public async Task<CustomResponseDto<List<UserWithVideoInterviewsDto>>> GetUsersWithVideoInterviewsSpecificPositionAsync(int positionId)
        {
            var users = await _userRepository.GetUsersWithVideoInterviewsSpecificPositionAsync(positionId);

            var usersDto = _mapper.Map<List<UserWithVideoInterviewsDto>>(users);

            return CustomResponseDto<List<UserWithVideoInterviewsDto>>.Success(200, usersDto);
        }
    }
}
