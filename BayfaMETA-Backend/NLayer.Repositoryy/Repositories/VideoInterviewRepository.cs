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
    public class VideoInterviewRepository : GenericRepository<VideoInterview>, IVideoInterviewRepository
    {
        public VideoInterviewRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<VideoInterview>> GetVideoInterviewsWithUser()
        {
            return await _context.VideoInterviews.Include(x => x.User).ToListAsync();
        }

        public async Task<VideoInterview> GetSingleVideoInterviewByIdWithScoresAsync(int videoInterviewId)
        {
            return await _context.VideoInterviews.Where(x => x.Id == videoInterviewId).Include(x => x.VideoInterviewScore).SingleOrDefaultAsync();
        }
    }
}
