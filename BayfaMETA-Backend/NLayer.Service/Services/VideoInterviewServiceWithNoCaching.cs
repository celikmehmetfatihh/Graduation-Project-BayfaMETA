using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repositoryy.Repositories;

namespace NLayer.Service.Services
{
    public class VideoInterviewServiceWithNoCaching : Service<VideoInterview>, IVideoInterviewService
    {
        private readonly IVideoInterviewRepository _videoInterviewRepository;
        //mapping
        private readonly IMapper _mapper;

        public VideoInterviewServiceWithNoCaching(IGenericRepository<VideoInterview> repository, IUnitOfWork unitOfWork, IMapper mapper, IVideoInterviewRepository videoInterviewRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _videoInterviewRepository = videoInterviewRepository;
        }

        public async Task<CustomResponseDto<List<VideoInterviewWithUserDto>>> GetVideoInterviewsWithUser()
        {
            var videoInterviews = await _videoInterviewRepository.GetVideoInterviewsWithUser();

            var videoInterviewsDto = _mapper.Map<List<VideoInterviewWithUserDto>>(videoInterviews);
            return CustomResponseDto<List<VideoInterviewWithUserDto>>.Success(200, videoInterviewsDto);
        }

        public async Task<CustomResponseDto<VideoInterviewWithScoreDto>> GetSingleVideoInterviewByIdWithScoresAsync(int videoInterviewId)
        {
            var videoInterview = await _videoInterviewRepository.GetSingleVideoInterviewByIdWithScoresAsync(videoInterviewId);

            var videoInterviewDto = _mapper.Map<VideoInterviewWithScoreDto>(videoInterview);

            return CustomResponseDto<VideoInterviewWithScoreDto>.Success(200, videoInterviewDto);
        }
    }
}
