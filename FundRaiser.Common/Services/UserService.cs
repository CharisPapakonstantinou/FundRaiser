using System.Threading.Tasks;
using FundRaiser.Common.Database;
using FundRaiser.Common.Interfaces;
using FundRaiser.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Common.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            return user;
        }

        public async Task<User> Update(int id, User userModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
            
            //Map base info
            user.FirstName = userModel.FirstName ?? user.FirstName;
            user.LastName = userModel.LastName ?? user.LastName;

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);

            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User> GetUser(int id, bool baseInfo = true)
        {
            var user = baseInfo
            ? await _context.Users.FirstOrDefaultAsync(p => p.Id == id)
            : await _context.Users
            .Include(u => u.Projects)
            .ThenInclude(u=> u.Rewards)
            .Include(u => u.Funds)
            .ThenInclude (u => u.Reward)
            .ThenInclude(u => u.Project)
            .ThenInclude(u=>u.Updates)
            .FirstOrDefaultAsync(u => u.Id == id);
            
            return user;
        }
    }
}

