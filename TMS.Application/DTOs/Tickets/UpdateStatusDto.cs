using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Common.Enums;

namespace TMS.Application.DTOs.Tickets
{
    public class UpdateStatusDto
    {
        [Required]
        [EnumDataType(typeof(StatusName), ErrorMessage = "Invalid Status value")]

        public StatusName Status { get; set; }

    }
}
