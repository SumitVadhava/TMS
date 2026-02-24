using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Common.Enums;

namespace TMS.Application.DTOs.Tickets
{
    public class CreateTicketDto
    {
        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        [EnumDataType(typeof(PriorityName), ErrorMessage = "Invalid Priority value")]

        public PriorityName Priority { get; set; } = PriorityName.MEDIUM;

        [Required]
        public int Created_By { get; set; }
        public int? Assigned_To { get; set; } = null;
    }
}
