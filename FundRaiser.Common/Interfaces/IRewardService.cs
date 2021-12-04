using FundRaiser.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaiser.Common.Interfaces
{
    public interface IRewardService
    {
        Task<Reward> Create(Reward rewardModel);
        Task<Reward> Update(int id, Reward rewardModel);
        Task<bool> Delete(int id);
        Task<List<Reward>> GetRewards(int projectId);
        Task<List<Reward>> GetBackerRewards(int userId, int projectId);
    }
}
