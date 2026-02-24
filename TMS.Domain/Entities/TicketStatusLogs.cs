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
    public class TicketStatusLogs : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Tickets")]
        public int Ticket_id { get; set; }  // delete cascade db context mathi aapel che

        [Required]
        [EnumDataType(typeof(StatusName),ErrorMessage = "Invalid Status value")]
        public StatusName Old_Status { get; set; }


        [Required]
        [EnumDataType(typeof(StatusName), ErrorMessage = "Invalid Status value")]

        public StatusName New_Status { get; set; }


        [ForeignKey("Users")]
        public int Changed_By { get; set; }


        public DateTime? Changed_At { get; set; } = DateTime.UtcNow.AddHours(5).AddMinutes(30);


        public Tickets Tickets { get; set; }

        public Users Users { get; set; }


    }
}