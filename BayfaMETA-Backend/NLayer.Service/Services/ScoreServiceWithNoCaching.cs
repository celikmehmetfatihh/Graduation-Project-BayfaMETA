using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repositoryy.Repositories;

namespace NLayer.Service.Services
{
    public class ScoreServiceWithNoCaching : Service<VideoInterviewScore>, IScoreService
    {
        private readonly IScoreRepository _scoreRepository;
        //mapping
        private readonly IMapper _mapper;

        public ScoreServiceWithNoCaching(IGenericRepository<VideoInterviewScore> repository, IUnitOfWork unitOfWork, IScoreRepository scoreRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _scoreRepository = scoreRepository;
            _mapper = mapper;
        }
    }
}
