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


        public async Task<IEnumerable<Users>> AddUser(Users user)
        {
            var result = await appDBContext.Users.AddAsync(user);
            await appDBContext.SaveChangesAsync();
            return (IEnumerable<Users>) result.Entity;
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

        public async Task<Users> GetUser(int id)
        {
            return await appDBContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Users> GetUserByEmail(string email)
        {
            return await appDBContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await appDBContext.Users.ToListAsync();
        }

        public async Task<IEnumerable<Users>> Search(string name)
        {
            IQueryable<Users> query = appDBContext.Users;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Users>> UpdateUser(Users user)
        {
            var result = await appDBContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if(result != null)
            {
                result.FirstName = user.FirstName;
                result.LastName = user.LastName;
                result.Email = user.Email;

                await appDBContext.SaveChangesAsync();
                return (IEnumerable<Users>)result;
            }

            return null;
        }
    }
}
