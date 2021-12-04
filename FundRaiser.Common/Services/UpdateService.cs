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

        public async Task<Update> Update(int updateId, Update update)
        {
            var updateFromDb = await _context.Updates.FirstOrDefaultAsync(u => u.Id == updateId);

            updateFromDb.Title = update.Title ?? updateFromDb.Title;
            updateFromDb.Description = update.Description ?? updateFromDb.Description;
            updateFromDb.PostDate = update.PostDate;

            await _context.SaveChangesAsync();

            return updateFromDb;
        }
    }
}

