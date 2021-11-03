using Brainer.NetCore.Models;
using Brainer.NetCore.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Repository
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly AppDBContext _appDBContext;

        public ApplicationUserRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public Task<ApplicationUser> AddUser(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetUser(string id)
        {
            return await _appDBContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _appDBContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await _appDBContext.ApplicationUsers.ToListAsync();
        }

        public Task<IEnumerable<ApplicationUser>> Search(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> UpdateUser(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
