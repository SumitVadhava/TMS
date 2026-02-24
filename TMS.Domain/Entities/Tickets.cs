using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Common.Enums;

namespace TMS.Domain.Entities
{
    public class Tickets : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [EnumDataType(typeof(StatusName), ErrorMessage = "Invalid Status value")]
        public StatusName? Status { get; set; } = StatusName.OPEN;

        [EnumDataType(typeof(PriorityName), ErrorMessage = "Invalid Priority value")]

        public PriorityName ? Priority { get; set; } = PriorityName.MEDIUM;

        [Required]
        [ForeignKey("CreatedByUser")]
        public int Created_By { get; set; }

        [ForeignKey("AssignedToUser")]
        public int? Assigned_To { get; set; } = null;

        public DateTime? Created_At { get; set; } = DateTime.UtcNow.AddHours(5).AddMinutes(30);

        public Users CreatedByUser { get; set; }

        public Users AssignedToUser { get; set; }


    }
}