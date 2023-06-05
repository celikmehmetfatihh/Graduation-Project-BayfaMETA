using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    public interface IScoreRepository : IGenericRepository<VideoInterviewScore>
    {
      //  Task<List<Score>> GetScoresWithUser();
    }
}
