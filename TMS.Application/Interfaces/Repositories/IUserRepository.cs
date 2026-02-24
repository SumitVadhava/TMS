using TMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Common.Enums;

namespace TMS.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<Users>? GetByEmailAsync(string email);
        Task<Roles>? GetRoleByNameAsync(RolesName Role);

    }
}
