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

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<UserDto>>> Get(int id)
        {
            var user = await _service.GetUser(id, false);

            return user == null
                ? NotFound(new ApiResult<UserDto>(null, false, $"No user found with Id = {id}."))
                : Ok(new ApiResult<UserDto>(new UserDto(user)));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<UserDto>>> Post(UserPostDto userPost)
        {
            var _user = new User()
            {
                FirstName = userPost.FirstName,
                LastName = userPost.LastName
            };

            var user = await _service.Create(_user);

            return user == null
               ? NotFound(new ApiResult<UserDto>(null, false, "Could not create user."))
               : Ok(new ApiResult<UserDto>(new UserDto(user)));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ApiResult<UserDto>>> Patch([FromRoute] int id, [FromBody] UserPostDto userPut)
        {
            var _user = new User()
            {
                FirstName = userPut.FirstName,
                LastName = userPut.LastName
            };

            var user = await _service.Update(id, _user);

            return user == null
                ? NotFound(new ApiResult<UserDto>(null, false, $"No user found with Id = {id}."))
                : Ok(new ApiResult<UserDto>(new UserDto(user)));
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
