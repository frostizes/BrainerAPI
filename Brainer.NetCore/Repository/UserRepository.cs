using Brainer.NetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDBContext appDBContext;

        public UserRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }


        public async Task<User> AddUser(User user)
        {
            var result = await appDBContext.Users.AddAsync(user);
            await appDBContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteUser(int id)
        {
            var result = appDBContext.Users.FirstOrDefault(u => u.Id == id);
            if(result != null)
            {
                appDBContext.Users.Remove(result);
                await appDBContext.SaveChangesAsync();
            }
        }

        public async Task<User> GetUser(int id)
        {
            return await appDBContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await appDBContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await appDBContext.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> Search(string email)
        {
            IQueryable<User> query = appDBContext.Users;
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(e => e.Email.Contains(email));
            }

            return await query.ToListAsync();
        }

        public async Task<User> UpdateUser(User user)
        {
            var result = await appDBContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
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
