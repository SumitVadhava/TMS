using TMS.Application.Common;
using TMS.Application.DTOs.Auth;
using TMS.Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using TMS.Domain.Common.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.Interfaces;
using TMS.Application.Interfaces.Services;
using TMS.Domain.Entities;


namespace TMS.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IService<Users> _service;
        private readonly IService<Roles> _roleservice;

        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IConfiguration _config;

        public AuthService(
            IService<Users> service,
            IUserRepository userRepository,
            IJwtTokenService jwtTokenService,
            IConfiguration config)
        {
             _service = service;
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _config = config;
        }


        public async Task<ServiceResult<AuthResponseDto>> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return ServiceResult<AuthResponseDto>.Fail("Invalid Crendationals");

            var role = await _roleservice.GetByIdAsync(user.Role_Id);

            if(role == null)
            {
                return ServiceResult<AuthResponseDto>.Fail("Role not found");
            }

            var token = _jwtTokenService.GenerateToken(user.Id.ToString(), role.Name);

            return ServiceResult<AuthResponseDto>.Ok(
                new AuthResponseDto
                {
                    Token = token,
                }   
                
            );
           
        }
    }
}