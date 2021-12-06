using FundRaiser.Common.Database;
using FundRaiser.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundRaiser.Common.Services
{
    public class RewardService
    {
        private readonly AppDbContext _context;

        public RewardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Reward> Create(Reward reward)
        {
            await _context.Rewards.AddAsync(reward);
            await _context.SaveChangesAsync();

            return reward;
        }

        public async Task<Reward> Update(int id, Reward rewardModel)
        {
            var reward = await _context.Rewards.FirstOrDefaultAsync(p => p.Id == id);

            reward.Title = rewardModel.Title ?? reward.Title;
            reward.Description = rewardModel.Description ?? reward.Description;
            reward.RequiredAmount = rewardModel.RequiredAmount == 0 ? reward.RequiredAmount : rewardModel.RequiredAmount;

            await _context.SaveChangesAsync();

            return reward;
        }

        public async Task<bool> Delete(int id)
        {
            var reward = await _context.Rewards.FirstOrDefaultAsync(r => r.Id == id);

            if (reward == null) return false;

            _context.Rewards.Remove(reward);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Reward>> GetRewards(int projectId)
        {
            return await _context.Rewards
                .Where(r => r.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<List<Reward>> GetBackerRewards(int userId, int projectId) 
        {
            return await _context.Funds
                .Include(f => f.Reward)
                .Where(f => f.UserId == userId && f.Reward.ProjectId == projectId)
                .Select(f => f.Reward)
                .ToListAsync();
        }

        public async Task<bool> BuyReward(int userId, int rewardId, int projectId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }
            var reward = await _context.Rewards.FirstOrDefaultAsync(r => r.Id == rewardId);

            if (reward == null)
            {
                return false;
            }

            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

            var isAlreadyBacker = await _context.Funds.Include(f => f.Reward).AnyAsync(f => f.UserId == userId && f.Reward.ProjectId == projectId);

            await _context.Funds.AddAsync(new Fund()
            {
                UserId = userId,
                RewardId = rewardId
            });

            project.CurrentAmount += reward.RequiredAmount;

            project.NumberOfBackers = isAlreadyBacker ? project.NumberOfBackers : project.NumberOfBackers + 1;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
