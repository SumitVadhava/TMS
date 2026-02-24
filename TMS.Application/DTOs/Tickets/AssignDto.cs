using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Common.Enums;

namespace TMS.Application.DTOs.Tickets
{
    public class AssignDto
    {
        [Required]
        public int userId { get; set; }

    }
}
