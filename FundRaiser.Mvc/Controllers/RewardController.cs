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
    public class RewardController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRewardService _rewardService;

        public RewardController(UserManager<User> userManager, IRewardService rewardService)
        {
            _userManager = userManager;
            _rewardService = rewardService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Reward/Create/{projectId}")]
        public async Task<IActionResult> Create(int projectId, RewardViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reward = new Reward
                {
                    ProjectId = projectId,
                    Title = model.Title,
                    Description = model.Description,
                    Price = model.Price
                };

                var result = await _rewardService.Create(reward);
                return RedirectToAction("Dashboard", "Project");
            }
            return View(model);
        }

        
        [Route("Reward/Buy/{rewardId}/{projectId}")]
        public async Task<IActionResult> Buy(int rewardId, int projectId)
        {
            var bought = await _rewardService.BuyReward(int.Parse(_userManager.GetUserId(User)), rewardId, projectId);
            return RedirectToAction("ProjectView", "Project", new { projectId = projectId });
        }
    }
}
