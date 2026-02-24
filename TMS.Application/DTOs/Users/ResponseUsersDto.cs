using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Application.DTOs.Users
{
    public class ResponseUsersDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }

        public int Role_Id { get; set; }
    }
}
