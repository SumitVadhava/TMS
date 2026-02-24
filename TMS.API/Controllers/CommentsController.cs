using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using TMS.Application.DTOs.Comments;
using TMS.Application.Interfaces;
using TMS.Domain.Entities;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class CommentsController : ControllerBase
    {
        private readonly IService<TicketComments> _commentService;
        private readonly IService<Tickets> _ticketService;

        public CommentsController(IService<TicketComments> commentService, IService<Tickets> ticketService)
        {
            _commentService = commentService;
            _ticketService = ticketService;
        }

        [HttpGet("tickets/{ticketId}/comments")]

        [Authorize]
        public async Task<IActionResult> GetCommentsForTicket(int ticketId)
        {
            var comments = (await _commentService.GetAllAsync())
                .Where(c => c.Ticket_id == ticketId);
            


            return Ok(new { error = false, data = comments, message = "Comments retrieved successfully" });
        }

        [HttpPost("tickets/{ticketId}/comments")]
        [Authorize]
        public async Task<IActionResult> AddComment(int ticketId, CommentDto dto)
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);

            var ticket = await _ticketService.GetByIdAsync(ticketId);


            if (ticket == null) return NotFound(new { error = true, message = "Ticket not found" });

            var comment = new TicketComments
            {
                Ticket_id = ticketId,
                User_id = userId,
                Comment = dto.Comment,
                Created_At = DateTime.UtcNow
            };

            var created = await _commentService.CreateAsync(comment);

            return Ok(new { error = false, data = created, message = "Comment added successfully" });
        }

        [HttpPatch("comments/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateComment(int id, CommentDto dto)
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            var comment = await _commentService.GetByIdAsync(id);


            if (comment == null) return NotFound();

            if (comment.User_id != userId && userRole != "MANAGER")
            {
                return Forbid();
            }

            var patched = await _commentService.PatchAsync(id, c => c.Comment = dto.Comment);


            return Ok(new { error = false, data = patched, message = "Comment updated successfully" });
        }

        [HttpDelete("comments/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            var comment = await _commentService.GetByIdAsync(id);


            if (comment == null) return NotFound();

            if (comment.User_id != userId && userRole != "MANAGER")
            {
                return Forbid();
            }

            await _commentService.DeleteAsync(id);
            return Ok(new { error = false, message = "Comment deleted successfully" });
        }
    }
}
