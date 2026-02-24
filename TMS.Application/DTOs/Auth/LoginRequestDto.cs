using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Application.DTOs.Auth
{
    public class LoginDto
    {
        public string Email { get; set; } = null!;

        [MinLength(6)]
        public string Password { get; set; } = null!;
    }
}
