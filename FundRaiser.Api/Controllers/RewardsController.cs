using FundRaiser.Common.Dto;
using FundRaiser.Common.Interfaces;
using FundRaiser.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FundRaiser.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardsController : ControllerBase
    {
         private readonly IRewardService _service;

        public RewardsController(IRewardService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<RewardDto>>>> Get(int projectId)
        {
            var tempList = await _service.GetRewards(projectId);

            if (tempList.Count == 0)
            {
                return NotFound(new ApiResult<List<RewardDto>>(null, false, $"Could not find rewards for project with Id = {projectId}."));
            }

            var rewardsList = tempList.Select(u=> new RewardDto(u)).ToList();

            return Ok(new ApiResult<List<RewardDto>>(rewardsList));
        }

        [HttpGet("/BackersReward")]
        public async Task<ActionResult<ApiResult<List<RewardDto>>>> Get( int userId, int projectId)
        {
            var tempList = await _service.GetBackerRewards(userId, projectId);

            if (tempList.Count == 0)
            {
                return new ApiResult<List<RewardDto>>(null, false, "No rewards found for this project Id.");
            }

            var rewardsList = tempList.Select(l => new RewardDto(l)).ToList();

            return Ok(new ApiResult<List<RewardDto>>(rewardsList));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<RewardDto>>> Post(RewardPostDto rewardPost)
        {
            var _reward = new Reward()
            {
                ProjectId = rewardPost.ProjectId,
                Title = rewardPost.Title,
                Description = rewardPost.Description,
                Price = (decimal)rewardPost.Price
            };

            var reward = await _service.Create(_reward);

            return reward == null
                ? NotFound(new ApiResult<RewardDto>(null, false, "Could not create a reward."))
                : Ok(new ApiResult<RewardDto>(new RewardDto(reward)));
        }

        [HttpPatch("{rewardid}")]
        public async Task<ActionResult<ApiResult<RewardDto>>> Patch([FromRoute] int rewardid, [FromBody] RewardPostDto rewardPut)
        {
            var reward = new Reward()
            {
                ProjectId = rewardPut.ProjectId,
                Title = rewardPut.Title,
                Description = rewardPut.Description,
                Price = (decimal)rewardPut.Price
            };

            var patchDto = await _service.Update(rewardid, reward);

            return patchDto == null
                ? NotFound(new ApiResult<RewardDto>(null, false, $"No reward found with Id = {rewardid}"))
                : Ok(new ApiResult<RewardDto>(new RewardDto(patchDto)));
        }

        [HttpPost("/BuyReward")]
        public async Task<ActionResult<object>> BuyReward([Required] int userId, [Required] int rewardId)
        {
            var buyReward = await _service.BuyReward(userId, rewardId);

            return buyReward == false
                ? NotFound(new ApiResult<object>(null, false, $"Could not buy reward with Id = {rewardId}."))
                : Ok(new ApiResult<object>(null, true, $"Successfully bought reward with Id = {rewardId}."));
        }

        [HttpDelete("{rewardId}")]
        public async Task<ActionResult<object>> Delete(int rewardId)
        {
            var reward = await _service.Delete(rewardId);

            return reward == false
                ? NotFound(new ApiResult<object>(null, false, $"Could not delete reward with Id = {rewardId}."))
                : Ok(new ApiResult<object>(null, true, $"Successfully deleted reward with Id = {rewardId}."));
        }
    }
}
