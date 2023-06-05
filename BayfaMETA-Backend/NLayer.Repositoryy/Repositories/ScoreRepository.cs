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
    public class ScoreRepository : GenericRepository<VideoInterviewScore>, IScoreRepository
    {
        public ScoreRepository(AppDbContext context) : base(context)
        {
        }

    }
}
