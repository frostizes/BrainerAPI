using Brainer.NetCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Repository
{
    public interface IApplicationUserRepository
    {
        Task<IEnumerable<ApplicationUser>> Search(string name);
        Task<IEnumerable<ApplicationUser>> GetUsers();
        Task<ApplicationUser> GetUser(string id);
        Task<ApplicationUser> GetUserByEmail(string email);
        Task<ApplicationUser> AddUser(ApplicationUser user);
        Task<ApplicationUser> UpdateUser(ApplicationUser user);
        Task DeleteUser(int id);
    }
}
