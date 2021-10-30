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

        private readonly AppDBContext appDBContext;

        public CustomerRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }


        public async Task<Customer> AddUser(Customer user)
        {
            var result = await appDBContext.Customer.AddAsync(user);
            await appDBContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteUser(int id)
        {
            var result = appDBContext.Customer.FirstOrDefault(u => u.Id == id);
            if(result != null)
            {
                appDBContext.Customer.Remove(result);
                await appDBContext.SaveChangesAsync();
            }
        }

        public async Task<Customer> GetUser(int id)
        {
            return await appDBContext.Customer.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Customer> GetUserByEmail(string email)
        {
            return await appDBContext.Customer.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<Customer>> GetUsers()
        {
            return await appDBContext.Customer.ToListAsync();
        }

        public async Task<IEnumerable<Customer>> Search(string email)
        {
            IQueryable<Customer> query = appDBContext.Customer;
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(e => e.Email.Contains(email));
            }

            return await query.ToListAsync();
        }

        public async Task<Customer> UpdateUser(Customer user)
        {
            var result = await appDBContext.Customer.FirstOrDefaultAsync(u => u.Id == user.Id);
            if(result != null)
            {
                result.FirstName = user.FirstName;
                result.LastName = user.LastName;
                result.Email = user.Email;

                await appDBContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }
}
