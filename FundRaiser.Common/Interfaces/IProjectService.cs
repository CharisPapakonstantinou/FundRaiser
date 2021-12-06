using FundRaiser.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaiser.Common.Interfaces
{
    public interface IProjectService
    {
        Task<Project> Create(Project projectModel);
        Task<Project> Update(int id, Project projectModel);
        Task<bool> Delete(int id);
        Task<Project> GetProject(int id, bool baseInfo = true);
        Task<List<Project>> GetProjects(int pageCount, int pageSize, int? userId = null, string title = null, Category? category = null);
        Task<List<Project>> GetFundedProjects(int userId);
        Task<List<Project>> GetTopFundedProjects(int count);
    }
}
