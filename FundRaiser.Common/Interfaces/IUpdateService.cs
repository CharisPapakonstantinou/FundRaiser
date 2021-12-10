using FundRaiser.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaiser.Common.Interfaces
{
    public interface IUpdateService
    {
        Task<Update> Create(Update update);
        Task<Update> Update(int updateId, Update update);
        Task<bool> Delete(int updateId);
        Task<List<Update>> GetUpdates(int projectId);
    }
}
