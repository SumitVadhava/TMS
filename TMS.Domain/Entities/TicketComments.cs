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
    public class TicketComments : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Tickets")]
        public int Ticket_id { get; set; } // delete cascade db context mathi aapel che

        [ForeignKey("Users")]
        public int? User_id { get; set; }

        [Required]
        public string Comment { get; set; }


        public DateTime? Created_At { get; set; } = DateTime.UtcNow.AddHours(5).AddMinutes(30);


        public Tickets Tickets { get; set; }

        public Users Users { get; set; }

    }
}