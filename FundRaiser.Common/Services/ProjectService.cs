using FundRaiser.Common.Database;
using FundRaiser.Common.Interfaces;
using FundRaiser.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaiser.Common.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;

        public ProjectService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Project> Create(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<Project> Update(int id, Project projectModel)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

            //Map base info
            project.Title = projectModel.Title ?? project.Title;
            project.Category = projectModel.Category;
            project.Description = projectModel.Description ?? project.Description;
            project.Goal = projectModel.Goal;
            project.EndDate = projectModel.EndDate;

            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<bool> Delete(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Project> GetProject(int id, bool baseInfo = true)
        {
            return baseInfo
                ? await _context.Projects.FirstOrDefaultAsync(p => p.Id == id)
                : await _context.Projects
                    .Include(p => p.Updates)
                    .Include(p => p.Rewards)
                    .Include(p => p.Media)
                    .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Project>> GetProjects(int pageCount, int pageSize, int? userId = null, string title = null, Category? category = null)
        {
            //if (pageSize <= 0) pageSize = 10;
            if (pageSize <= 0 || pageSize > 10) pageSize = 10;
            if (pageCount <= 0) pageCount = 1;

            return await _context.Projects 
                .Where(p =>
                    (userId == null || p.UserId == userId)
                    && (title == null || p.Title.ToLower().Contains(title.ToLower()))
                    && (category == null || p.Category == category))
                .OrderByDescending(p => p.Id)
                .Skip((pageCount - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Project>> GetFundedProjects(int userId)
        {
            return await _context.Funds
                .Include(f => f.Reward)
                .ThenInclude(r => r.Project)
                .Where(f => f.UserId == userId)
                .Select(f => f.Reward.Project)
                .ToListAsync();
        }
    }
}
