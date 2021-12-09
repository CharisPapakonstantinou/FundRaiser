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
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectsController(IProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<ProjectDto>>>> Get(int pageCount, int pageSize, int? userId = null, string title = null, Category? category = null)
        {

            var _list = await _service.GetProjects(pageCount, pageSize, userId, title, category);

            if (_list.Any() == false)
            {
                return NotFound(new ApiResult<object>(null,false,"Could not find any project"));
            }

            var list = _list.Select(l => new ProjectDto(l)).ToList();

            return Ok(new ApiResult<List<ProjectDto>>(list));
        }

        [HttpGet, Route("{id}")]
        public async Task<ActionResult<ApiResult<ProjectDto>>> GetProject(int id)
        {
            var _project = await _service.GetProject(id, false);

            return _project == null
                ? NotFound(new ApiResult<ProjectDto>(null, false, $"Could not find project with Id = {id}."))
                : Ok(new ApiResult<ProjectDto>(new ProjectDto(_project)));
        }

        [HttpGet("/FundedByUsers/{Id}")]
        public async Task<ActionResult<List<ProjectDto>>> GetFundedProjects(int Id)
        {
            var _list = await _service.GetFundedProjects(Id);

            if (_list == null)
            {
                return NotFound($"User with id = {Id} has not funded projects.");
            }

            var list = _list.Select(l => new ProjectDto(l)).ToList();
      
            return Ok(new ApiResult<List<ProjectDto>>(list));
        }

        [HttpPatch]
        public async Task<ActionResult<ApiResult<ProjectDto>>> Patch(int projectId, ProjectUpdateDto projectUpdateDto)
        {
            var projectDb = await _service.GetProject(projectId);

            if (projectDb == null)
            {
                return NotFound(new ApiResult<ProjectDto>(null, false, $"Could not find project with Id = {projectId}"));
            }

            var _project = new Project()
            {
                Title = projectUpdateDto.Title,
                Description = projectUpdateDto.Description,
                Category = projectUpdateDto.Category,
                Goal = projectUpdateDto.Goal ?? projectDb.Goal,
                EndDate = projectUpdateDto.EndDate
            };

            var project = await _service.Update(projectId, _project);

            return Ok(new ApiResult<ProjectDto>(new ProjectDto(project))); 
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<ProjectDto>>> Post(ProjectPostDto projectDto)
        {
            var _project = new Project()
            {
                UserId = projectDto.UserId,
                Title = projectDto.Title,
                Description = projectDto.Description,
                Category = projectDto.Category,
                Goal = (decimal)projectDto.Goal,
                EndDate = projectDto.EndDate
            };

            var project = await _service.Create(_project);

            return project == null
                ? NotFound(new ApiResult<ProjectDto>(null, false, "Could not create project."))
                : Ok(new ApiResult<ProjectDto>(new ProjectDto(project)));
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<object>>> Delete(int id)
        {
            var project = await _service.Delete(id);

            return project == false
                ? NotFound(new ApiResult<object>(null, false, $"Could not delete project with Id = {id}"))
                : Ok(new ApiResult<object>(null, true, $"Successfully deleted project with Id = {id}"));
        }
    }
}
