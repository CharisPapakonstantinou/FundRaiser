using FundRaiser.Common.Dto;
using FundRaiser.Common.Interfaces;
using FundRaiser.Common.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundRaiser.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdatesController : ControllerBase
    {
        private readonly IUpdateService _service;

        public UpdatesController(IUpdateService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<UpdateDto>>>> Get(int projectId)
        {
            var tempList = await _service.GetUpdates(projectId);

            if (tempList.Count == 0)
            {
                return NotFound(new ApiResult<List<UpdateDto>>(null, false, $"No updates found for project with Id = {projectId}"));
            }

            var updatesList = tempList.Select(update => MyMapper.UpdateToUpdateDto(update)).ToList();

            return Ok(new ApiResult<List<UpdateDto>>(updatesList));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<UpdateDto>>> Post([FromBody] UpdatePostDto updatePostDto)
        {
            var _update = MyMapper.UpdatePostDtoToUpdate(updatePostDto);

            var update = await _service.Create(_update);

            return update == null
                ? NotFound(new ApiResult<UpdateDto>(null, false, $"Could not create update."))
                : Ok(new ApiResult<UpdateDto>(MyMapper.UpdateToUpdateDto(update)));
        }

        [HttpPatch("{updateId}")]
        public async Task<ActionResult<ApiResult<UpdateDto>>> Patch([FromRoute] int updateId, UpdatePatchDto updatePatchDto)
        {
            var _update = MyMapper.UpdatePatchDtoToUpdate(updatePatchDto);
            
            var update = await _service.Update(updateId, _update);

            return update == null
                ? NotFound(new ApiResult<UpdateDto>(null, false, $"No update found with Id = {updateId}"))
                : Ok(new ApiResult<UpdateDto>(MyMapper.UpdateToUpdateDto(update)));
        }

        [HttpDelete("{updateId}")]
        public async Task<ActionResult<ApiResult<object>>> Delete([FromRoute] int updateId)
        {
            var update = await _service.Delete(updateId);

            return update == false
                ? NotFound(new ApiResult<object>(null, false, $"Could not delete update with Id = {updateId}."))
                : Ok(new ApiResult<object>(null, true, $"Successfully deleted update with Id = {updateId}."));
        }
    }
}
