using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repositoryy.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User> GetSingleUserByIdWithVideoInterviewsAsync(int userId)
        {
            return await _context.Users.Include(x => x.videoInterviews).Where(x => x.Id == userId).SingleOrDefaultAsync();
        }

        public async Task<List<User>> GetUsersWithVideoInterviewsSpecificPositionAsync(int positionId)
        {
            return await _context.Users.Include(x => x.videoInterviews.Where(v => v.PositionId == positionId)).Where(x => (x.videoInterviews.Where(v => v.PositionId == positionId)).ToList().Count > 0).ToListAsync();
        }
    }
}
