using FundRaiser.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaiser.Common.Interfaces
{
    interface IUpdateService
    {
        Task<Update> Create(Update _update);
        Task<Update> Update(int updateId, Update _update);
        Task<bool> Delete(int updateId);
        Task<List<Update>> GetUpdates(int projectId);
    }
}
