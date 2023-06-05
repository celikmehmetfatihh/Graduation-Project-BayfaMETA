using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IUserService:IService<User>
    {
        public Task<CustomResponseDto<UserWithVideoInterviewsDto>> GetSingleUserByIdWithVideoInterviewsAsync(int userId);

        public Task<CustomResponseDto<List<UserWithVideoInterviewsDto>>> GetUsersWithVideoInterviewsSpecificPositionAsync(int positionId);
    }
}
