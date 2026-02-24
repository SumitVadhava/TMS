using TMS.Application.Common;
using TMS.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ServiceResult<AuthResponseDto>> LoginAsync(LoginDto dto);
    }
}