using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<User> GetSingleUserByIdWithVideoInterviewsAsync(int userId);

        Task<List<User>> GetUsersWithVideoInterviewsSpecificPositionAsync(int positionId);
    }
}
