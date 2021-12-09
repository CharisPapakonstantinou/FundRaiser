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
    public class RewardsController : ControllerBase
    {
        private readonly IRewardService _service;
        private readonly IMapper _mapper;
        public RewardsController(IRewardService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<RewardDto>>>> Get(int projectId)
        {
            var tempList = await _service.GetRewards(projectId);

            if (tempList.Count == 0)
            {
                return NotFound(new ApiResult<List<RewardDto>>(null, false, $"Could not find rewards for project with Id = {projectId}."));
            }

            var rewardsList = tempList.Select(reward => _mapper.Map<RewardDto>(reward)).ToList();

            return Ok(new ApiResult<List<RewardDto>>(rewardsList));
        }

        [HttpGet("/BackersReward")]
        public async Task<ActionResult<ApiResult<List<RewardDto>>>> Get(int userId, int projectId)
        {
            var tempList = await _service.GetBackerRewards(userId, projectId);

            if (tempList.Count == 0)
            {
                return new ApiResult<List<RewardDto>>(null, false, $"User with Id = {userId} has not purchase any reward for the project with Id = {projectId}");
            }

            var rewardsList = tempList.Select(reward => _mapper.Map<RewardDto>(reward)).ToList();

            return Ok(new ApiResult<List<RewardDto>>(rewardsList));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<RewardDto>>> Post(RewardPostDto rewardPostDto)
        {
            var _reward = _mapper.Map<Reward>(rewardPostDto);

            var reward = await _service.Create(_reward);

            return reward == null
                ? NotFound(new ApiResult<RewardDto>(null, false, "Could not create a reward."))
                : Ok(new ApiResult<RewardDto>(_mapper.Map<RewardDto>(reward)));
        }

        [HttpPatch("{rewardId}")]
        public async Task<ActionResult<ApiResult<RewardDto>>> Patch([FromRoute] int rewardId, [FromBody] RewardPostDto rewardPostDto)
        {
            var _reward = _mapper.Map<Reward>(rewardPostDto);

            var reward = await _service.Update(rewardId, _reward);

            return reward == null
                ? NotFound(new ApiResult<RewardDto>(null, false, $"No reward found with Id = {rewardId}"))
                : Ok(new ApiResult<RewardDto>(_mapper.Map<RewardDto>(reward)));
        }

        [HttpPost("/BuyReward/{rewardId}")]
        public async Task<ActionResult<object>> BuyReward(int userId, int rewardId, int projectId)
        {
            var buyReward = await _service.BuyReward(userId, rewardId, projectId);

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
