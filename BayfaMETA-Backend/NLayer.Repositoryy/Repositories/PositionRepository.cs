using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repositoryy.Repositories
{
    public class PositionRepository : GenericRepository<Position>, IPositionRepository
    {
        public PositionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Position> GetAllPositionsWithUsers(int positionId)
        {
            return await _context.Positions.Include(x => x.userPositions).Where(x => x.Id == positionId).SingleOrDefaultAsync();
        }
    }
}
