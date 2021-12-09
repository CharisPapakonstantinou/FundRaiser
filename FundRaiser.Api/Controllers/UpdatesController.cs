using AutoMapper;
using FundRaiser.Common.Dto;
using FundRaiser.Common.Interfaces;
using FundRaiser.Common.Models;
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
        private readonly IMapper _mapper;

        public UpdatesController(IUpdateService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<UpdateDto>>>> Get(int projectId)
        {
            var tempList = await _service.GetUpdates(projectId);

            if (tempList.Count == 0)
            {
                return NotFound(new ApiResult<List<UpdateDto>>(null, false, $"No updates found for project with Id = {projectId}"));
            }

            var updatesList = tempList.Select(update => _mapper.Map<UpdateDto>(update)).ToList();

            return Ok(new ApiResult<List<UpdateDto>>(updatesList));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<UpdateDto>>> Post([FromBody] UpdatePostDto updatePost)
        {
            var _update = _mapper.Map<Update>(updatePost);

            var update = await _service.Create(_update);

            return update == null
                ? NotFound(new ApiResult<UpdateDto>(null, false, $"Could not create update."))
                : Ok(new ApiResult<UpdateDto>(_mapper.Map<UpdateDto>(update)));
        }

        [HttpPatch("{updateId}")]
        public async Task<ActionResult<ApiResult<UpdateDto>>> Patch([FromRoute] int updateId, UpdatePatchDto updatePatch)
        {
            var _update = _mapper.Map<Update>(updatePatch);

            var update = await _service.Update(updateId, _update);

            return update == null
                ? NotFound(new ApiResult<UpdateDto>(null, false, $"No update found with Id = {updateId}"))
                : Ok(new ApiResult<UpdateDto>(_mapper.Map<UpdateDto>(update)));
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
