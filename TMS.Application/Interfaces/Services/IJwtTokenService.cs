using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Common.Enums;

namespace TMS.Application.Interfaces.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId,RolesName Role = RolesName.USER);

    }
}
