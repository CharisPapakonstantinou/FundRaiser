using FundRaiser.Common.Database;
using FundRaiser.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundRaiser.Common.Interfaces;


namespace FundRaiser.Common.Services
{
    public class UpdateService :IUpdateService
    {
        private readonly AppDbContext _context;

        public UpdateService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Update> Create(Update update)
        {
            await _context.Updates.AddAsync(update);
            await _context.SaveChangesAsync();
        
            return update;
        }

        public async Task<bool> Delete(int updateId)
        {
            var update = await _context.Updates.FirstOrDefaultAsync(u => u.Id == updateId);

            if (update == null)
            {
                return false;
            }

            _context.Updates.Remove(update);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Update>> GetUpdates(int projectId)
        {
            return await _context.Updates
            .Where(u => u.Project.Id == projectId)
            .ToListAsync();
        }

        public async Task<Update> Update(int updateId, Update _update)
        {
            var update = await _context.Updates.FirstOrDefaultAsync(u => u.Id == updateId);

            update.Title = _update.Title ?? update.Title;
            update.Description = _update.Description ?? update.Description;
            update.PostDate = _update.PostDate;

            await _context.SaveChangesAsync();

            return update;
        }
    }
}

