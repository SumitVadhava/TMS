using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using TMS.Domain.Entities;
using TMS.Infrastructure.Data;
using TMS.Application.Interfaces.Repositories;
using TMS.Domain.Common.Enums;

namespace TMS.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly AppDbContext _db;
        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Users>? GetByEmailAsync(string email)
        {
            return await _db.Users.Include(u => u.Roles).FirstOrDefaultAsync(x => x.Email == email);

        }
        public async Task<Roles>? GetRoleByNameAsync(RolesName Role)
        {
            return await _db.Roles.FirstOrDefaultAsync(r => r.Name == Role);
        }

      
    }
}
