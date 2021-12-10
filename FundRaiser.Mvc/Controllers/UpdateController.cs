using FundRaiser.Common.Interfaces;
using FundRaiser.Common.Models;
using FundRaiser.Mvc.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundRaiser.Mvc.Controllers
{
    public class UpdateController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUpdateService _updateService;

        public UpdateController(UserManager<User> userManager, IUpdateService updateService)
        {
            _userManager = userManager;
            _updateService = updateService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Update/Create/{projectId}")]
        public async Task<IActionResult> Create(int projectId, UpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var update = new Update
                {
                    ProjectId = projectId,
                    Title = model.Title,
                    Description = model.Description
                };

                var result = await _updateService.Create(update);
                return RedirectToAction("Dashboard", "Project");
            }
            return View(model);
        }
    }
}
