using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Application.DTOs;
using TMS.Application.DTOs.Users;
using TMS.Application.DTOs.Users;
using TMS.Application.Interfaces;
using TMS.Domain.Entities;
using TMS.Domain.Entities;

namespace TMS.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IService<Users> _service;
        private readonly IService<Roles> _roleservice;

        public UsersController(IService<Users> service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "MANAGER")]
        public async Task<IActionResult> GetAll()
        { 
            var users = await _service.GetAllAsync();
            var result = new List<ResponseUsersDto>();

            foreach (var user in users)
            {
                result.Add(new ResponseUsersDto
                {
                    Id = user.Id,
                    UserName = user.Name,
                    Email = user.Email,
                    Role_Id = user.Role_Id,
                });
            }

            return Ok(new
            {
                success = true,
                data = result
            });
        }


        [HttpPost]
        [Authorize(Roles = "MANAGER")]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            var roleId = _roleservice.GetAllAsync()
                .Result.FirstOrDefault(r => r.Name == dto.Role).Id;

            var entity = new Users
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role_Id = roleId,
            };


            var created = await _service.CreateAsync(entity);
            return Ok(created);
        }

        
    }
}
