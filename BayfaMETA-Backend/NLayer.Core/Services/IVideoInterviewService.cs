using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IVideoInterviewService:IService<VideoInterview>
    {
        public Task<CustomResponseDto<List<VideoInterviewWithUserDto>>> GetVideoInterviewsWithUser();

        public Task<CustomResponseDto<VideoInterviewWithScoreDto>> GetSingleVideoInterviewByIdWithScoresAsync(int videoInterviewId);
    }
}