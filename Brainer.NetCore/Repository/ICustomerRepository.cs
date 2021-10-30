using Brainer.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> Search(string name);
        Task<IEnumerable<Customer>> GetUsers();
        Task<Customer> GetUser(int id);
        Task<Customer> GetUserByEmail(string email);
        Task<Customer> AddUser(Customer user);
        Task<Customer> UpdateUser(Customer user);
        Task DeleteUser(int id);
    }
}
