using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateFilterAttribute]
    public class UsersController : CustomBaseController
    {
        private readonly IUserService _userService;
        private readonly IService<UserPosition> _userPositionService;
        private readonly IService<Position> _positionService;
        private readonly IMapper _mapper;



        public UsersController(IUserService userService, IMapper mapper, IService<UserPosition> userPositionService,
            IService<Position> positionService)
        {
            _userService = userService;
            _mapper = mapper;
            _userPositionService = userPositionService;
            _positionService = positionService;
        }

        /// <summary>
        /// Get a user from database with its video interview details.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetSingleUserByIdWithVideoInterviewsAsync(int userId)
        {

            return CreateActionResult(await _userService.GetSingleUserByIdWithVideoInterviewsAsync(userId));
        }

        /// <summary>
        /// Get users of a specific position with its video interview submitted to that position.
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{positionId}")]
        public async Task<IActionResult> GetUsersWithVideoInterviewsSpecificPositionAsync(int positionId)
        {

            return CreateActionResult(await _userService.GetUsersWithVideoInterviewsSpecificPositionAsync(positionId));
        }

        /// <summary>
        /// Get email and password from frontend. Check if user exists in the database.
        /// If user exists return user details,
        /// else return null
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            var users = await _userService.GetAllAsync();
            var currentUser = _mapper.Map<User>(userDto);
            UserDto returnUser = null;
            User tempUser = null;



            foreach (var user in users)
            {
                if (user.email.Equals(currentUser.email) && user.password.Equals(currentUser.password))
                {
                    returnUser = _mapper.Map<UserDto>(user);
                    tempUser = user;
                }
            }

            if (returnUser != null)
            {
                var userPositions = await _userPositionService.Where(x => x.UserId == returnUser.Id).ToListAsync();
                List<PositionWithCompletionInformation> positionDtos = new List<PositionWithCompletionInformation>();

                foreach (var position in userPositions)
                {
                    Position p = await _positionService.GetByIdAsync(position.PositionId);
                    PositionDto pDto = _mapper.Map<PositionDto>(p);
                    PositionWithCompletionInformation positionWithCompletion =
                        _mapper.Map<PositionWithCompletionInformation>(pDto);
                    positionWithCompletion.userPosition = _mapper.Map<UserPositionDto>(position);
                    positionDtos.Add(positionWithCompletion);
                }


                UserWithPositionsDto newReturnUser = _mapper.Map<UserWithPositionsDto>(tempUser);
                newReturnUser.positions = positionDtos;

                return CreateActionResult(CustomResponseDto<UserWithPositionsDto>.Success(201, newReturnUser));

            }




            return CreateActionResult(CustomResponseDto<UserDto>.Success(201, returnUser));
        }


        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetAppliedPositionsOfAUser(int userId)
        {
            var positionList = _userPositionService.Where(user => user.UserId == userId).ToList();
            List<int> posionIds = new();
            foreach (var pos in positionList)
            {
                posionIds.Add(pos.PositionId);
            }

            var appliedPositions = _positionService.Where(position => posionIds.Contains(position.Id));
            var positionDto = _mapper.Map<List<PositionDto>>(appliedPositions.ToList());

            return CreateActionResult(CustomResponseDto<List<PositionDto>>.Success(201, positionDto));
        }

        [HttpGet("[action]/{userId}/{positionId}")]
        public async Task<IActionResult> GetCompletionOfUser(int userId, int positionId)
        {
            var userPosition = _userPositionService.Where(up => up.UserId == userId && up.PositionId == positionId)
                .FirstOrDefault();

            var userPositionDto = _mapper.Map<UserCompletionDto>(userPosition);
            return CreateActionResult(CustomResponseDto<UserCompletionDto>.Success(201, userPositionDto));
        }

        [HttpGet("[action]/{userId}/{positionId}")]
        public async Task<IActionResult> IsUserInPosition(int userId, int positionId)
        {
            var userPosition = _userPositionService.Where(up => up.UserId == userId && up.PositionId == positionId)
                .FirstOrDefault();
            var userPositionDto = _mapper.Map<UserCompletionDto>(userPosition);
            return CreateActionResult(CustomResponseDto<UserCompletionDto>.Success(201, userPositionDto));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDto userDto)
        {
            var user = await _userService.Where(u => u.email == userDto.Email).FirstOrDefaultAsync();
            user.password = userDto.Password;

            await _userService.UpdateAsync(user);
            return Ok();

        }

        [HttpPost("[action]")] 
        public async Task<IActionResult> Register(UserDto userDto)
        {
            userDto.role = "applicant";
            var user = await _userService.AddAsync(_mapper.Map<User>(userDto));
            //mapping
            var userDtos = _mapper.Map<UserDto>(user);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(201, userDtos)); 
        }
    }
}
