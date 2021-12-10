using FundRaiser.Common.Interfaces;
using FundRaiser.Common.Models;
using FundRaiser.Mvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FundRaiser.Mvc.Controllers
{
    public class ProjectController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IProjectService _projectService;
        private readonly IUpdateService _updateService;
        private readonly IRewardService _rewardService;
        private readonly IHostEnvironment _hostEnvironment;


        public ProjectController(UserManager<User> userManager, IProjectService projectService,
                            IUpdateService updateService, IRewardService rewardService, IHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _projectService = projectService;
            _updateService = updateService;
            _rewardService = rewardService;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateViewModel model)
        {
            var img = model.ProductImage;
            if (ModelState.IsValid)
            {
                var project = new Project
                {
                    UserId = int.Parse(_userManager.GetUserId(User)),
                    Title = model.Title,
                    Description = model.Description,
                    Category = model.Category,
                    EndDate = model.EndDate,
                    Goal = model.Goal,
                    Media = new List<Media>()
                };

                if (img != null)
                {
                    var uniqueFileName = GetUniqueFileName(img.FileName);
                    var uploads = Path.Combine(_hostEnvironment.ContentRootPath + "\\wwwroot", "images");
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    img.CopyTo(new FileStream(filePath, FileMode.Create));

                    project.Media.Add(new Media
                    {
                        ProjectId = project.Id,
                        Path = uniqueFileName,
                        MediaType = MediaType.Image
                    });
                }

                var result = await _projectService.Create(project);
                RedirectToAction("Dashboard");
            }
            return View(model);
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        [Route("Project/Category/{id}")]
        public async Task<IActionResult> Category(int id)
        {
            var category = (Category)id;
            var projects = await _projectService.GetProjects(1, 10, category: category);
            return View(projects);
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            var userId = int.Parse(_userManager.GetUserId(User));
            var projects = await _projectService.GetProjects(1, 10, userId: userId);
            return View(projects);
        }

        [Authorize]
        [Route("Project/Delete/{projectId}")]
        public async Task<IActionResult> Delete(int projectId)
        {
            var deleted = await _projectService.Delete(projectId);
            return RedirectToAction("Dashboard");
        }

        [Authorize]
        [Route("Project/Update/{projectId}")]
        public async Task<IActionResult> Update (int projectId)
        {
            ViewBag.Project = await _projectService.GetProject(projectId);
            return View();
        }

        [Authorize]
        [Route("Project/Update/{projectId}")]
        [HttpPost]
        public async Task<IActionResult> Update(int projectId, ProjectUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var project = await _projectService.GetProject(projectId);
                var updatedProject = new Project
                {
                    Title = model.Title,
                    Description = model.Description,
                    Category = model.Category ?? project.Category,
                    EndDate = model.EndDate ?? project.EndDate,
                    Goal = model.Goal ?? project.Goal
                };
                await _projectService.Update(projectId, updatedProject);
                return RedirectToAction("Dashboard");
            }
            return View(model);
        }

        [Route("Project/ProjectView/{projectId}")]
        public async Task<IActionResult> ProjectView(int projectId)
        {
            var project = await _projectService.GetProject(projectId);
            ViewBag.Updates = await _updateService.GetUpdates(projectId);
            ViewBag.Rewards = await _rewardService.GetRewards(projectId);
            return View(project);
        }
    }
}
