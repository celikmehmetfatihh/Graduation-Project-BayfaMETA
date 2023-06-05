using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Service.Mapping
{
    /// <summary>
    /// Class for mapping Dtos to Models.
    /// </summary>
    public class MapProfile : Profile
    {
        public MapProfile()
        {
   
            CreateMap<VideoInterview, VideoInterviewDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<VideoInterviewScore, VideoInterviewScoreDto>().ReverseMap();
            CreateMap<VideoInterview, VideoInterviewWithUserDto>().ReverseMap();
            CreateMap<User, UserWithVideoInterviewsDto>().ReverseMap();
            CreateMap<VideoInterviewScore, ScoreWithUserDto>();
            CreateMap<VideoInterview, VideoInterviewWithScoreDto>().ReverseMap();
            CreateMap<Position, PositionDto>().ReverseMap();
            CreateMap<StageConfiguration, StageConfigurationDto>().ReverseMap();
            CreateMap<TechnicalConfiguration, TechnicalConfigurationDto>().ReverseMap();
            CreateMap<VideoInterviewConfiguration, VideoInterviewConfigurationDto>().ReverseMap();
            CreateMap<PositionWithUsersDto, Position>().ReverseMap();
            CreateMap<PositionPostingDto, Position>();
            CreateMap<PositionPostingDto, ResumeConfiguration>();
            CreateMap<PositionPostingDto, VideoInterviewConfiguration>();
            CreateMap<PositionPostingDto, TechnicalConfiguration>();
            CreateMap<UserPosition, UserPositionDto>().ReverseMap();
            CreateMap<UserWithPositionsDto, User>().ReverseMap();
            CreateMap<PositionWithCompletionInformation, PositionDto>().ReverseMap();
            CreateMap<Resume, ResumeDto>().ReverseMap();
            CreateMap<InterviewQuestion, InterviewQuestionDto>().ReverseMap(); 
            CreateMap<QuestionBank, QuestionBankDto>().ReverseMap(); 
            CreateMap<QuestionBank, QuestionBankWithInterviewQuestionDto>().ReverseMap();
            CreateMap<ResumeConfiguration, ResumeConfigurationDto>().ReverseMap();
            CreateMap<UserPosition, UserCompletionDto>().ReverseMap();
        }
    }
}
