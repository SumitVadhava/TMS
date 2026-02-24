//using TMS.Application.DTOs.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.Application.DTOs.Tickets;
using TMS.Application.Interfaces;
using TMS.Application.Interfaces.Services;
using TMS.Domain.Common.Enums;
using TMS.Domain.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly IService<Tickets> _service;
        public TicketsController(IService<Tickets> service)
        {
            _service = service;
        }

        

        [HttpPost]
        [Authorize(Roles = "MANAGER,USER")]


        public async Task<IActionResult> Create(CreateTicketDto dto)
        {
            var userId = User.FindFirst("Sub").Value;
       
            var entity = new Tickets
            {
               Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                Created_By = int.Parse(userId), 
                Assigned_To = dto.Assigned_To,
                Created_At = DateTime.UtcNow,
                Status = StatusName.OPEN
            };


            var created = await _service.CreateAsync(entity);
            return Ok(created);
        }

        [HttpGet]
        [Authorize(Roles = "MANAGER,USER")]

        public async Task<IActionResult> GetAll()
            => Ok(new { error = false, data = await _service.GetAllAsync(), message = "Operation Successfull" });

       

        [HttpPatch("{id}/assign")]

        [Authorize(Roles = "MANAGER,SUPPORT")]
        public async Task<IActionResult> Assign(int id, AssignDto dto)
        {
            var patched = await _service.PatchAsync(id, t => t.Assigned_To = dto.userId);

            return patched != null ? Ok(new { error = false, data = patched, message = "Ticket assigned successfully" }) : NotFound();
        }

        [HttpPatch("{id}/status")]

        [Authorize(Roles = "MANAGER,SUPPORT")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateStatusDto dto)
        {
            var patched = await _service.PatchAsync(id, t => t.Status = dto.Status);
            if (patched == null)
                return NotFound();

            return patched != null ? Ok(new { error = false, data = patched, message = "Ticket status updated successfully" }) : NotFound();
        }

        [HttpDelete("{id}")]

        [Authorize(Roles = "MANAGER")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted != null ? Ok() : NotFound();
        }

    }

}
