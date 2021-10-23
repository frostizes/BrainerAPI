using Brainer.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> Search(string name);
        Task<IEnumerable<Users>> GetUsers();
        Task<Users> GetUser(int id);
        Task<Users> GetUserByEmail(string email);
        Task<IEnumerable<Users>> AddUser(Users user);
        Task<IEnumerable<Users>> UpdateUser(Users user);
        Task DeleteUser(int id);
    }
}
