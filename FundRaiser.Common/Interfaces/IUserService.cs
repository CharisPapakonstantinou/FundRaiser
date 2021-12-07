using FundRaiser.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaiser.Common.Interfaces
{
    public interface IUserService
    {
        Task<User> Create(User userModel);
        Task<User> Update(int id, User userModel);
        Task<bool> Delete(int id);
        Task<User> GetUser(int id, bool baseInfo = true);
    }
}
