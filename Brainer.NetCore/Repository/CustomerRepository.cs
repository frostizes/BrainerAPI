using Brainer.NetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly AppDBContext _appDBContext;

        public CustomerRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public async Task<Customer> AddUser(Customer user)
        {
            var result = await _appDBContext.Customers.AddAsync(user);
            await _appDBContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteUser(int id)
        {
            var result = _appDBContext.Customers.FirstOrDefault(u => u.Id == id);
            if(result != null)
            {
                _appDBContext.Customers.Remove(result);
                await _appDBContext.SaveChangesAsync();
            }
        }

        public async Task<Customer> GetUser(int id)
        {
            return await _appDBContext.Customers.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Customer> GetUserByEmail(string email)
        {
            return await _appDBContext.Customers.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<Customer>> GetUsers()
        {
            return await _appDBContext.Customers.ToListAsync();
        }

        public async Task<IEnumerable<Customer>> Search(string email)
        {
            IQueryable<Customer> query = _appDBContext.Customers;
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(e => e.Email.Contains(email));
            }

            return await query.ToListAsync();
        }

        public async Task<Customer> UpdateUser(Customer user)
        {
            var result = await _appDBContext.Customers.FirstOrDefaultAsync(u => u.Id == user.Id);
            if(result != null)
            {
                result.FirstName = user.FirstName;
                result.LastName = user.LastName;
                result.Email = user.Email;

                await _appDBContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }
}
