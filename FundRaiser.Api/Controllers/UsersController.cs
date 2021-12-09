using AutoMapper;
using FundRaiser.Common.Dto;
using FundRaiser.Common.Interfaces;
using FundRaiser.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FundRaiser.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UsersController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<UserDto>>> Get(int id)
        {
            var user = await _service.GetUser(id, false);

            return user == null
                ? NotFound(new ApiResult<UserDto>(null, false, $"No user found with Id = {id}."))
                : Ok(new ApiResult<UserDto>(_mapper.Map<UserDto>(user)));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<UserDto>>> Post(UserPostDto userPost)
        {
            var _user = _mapper.Map<User>(userPost);

            var user = await _service.Create(_user);

            return user == null
               ? NotFound(new ApiResult<UserDto>(null, false, "Could not create user."))
               : Ok(new ApiResult<UserDto>(_mapper.Map<UserDto>(user)));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ApiResult<UserDto>>> Patch([FromRoute] int id, [FromBody] UserPostDto userPatch)
        {
            var _user = _mapper.Map<User>(userPatch);

            var user = await _service.Update(id, _user);

            return user == null
                ? NotFound(new ApiResult<UserDto>(null, false, $"No user found with Id = {id}."))
                : Ok(new ApiResult<UserDto>(_mapper.Map<UserDto>(user)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<object>>> Delete(int id)
        {
            var user = await _service.Delete(id);

            return user == false
                 ? NotFound(new ApiResult<object>(null, false, $"Could not delete user with Id = {id}"))
                 : Ok(new ApiResult<object>(null, true, $"Successfully deleted user with Id = {id}"));
        }
    }
}
