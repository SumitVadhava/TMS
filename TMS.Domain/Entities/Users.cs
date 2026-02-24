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
    [Index(nameof(Email), IsUnique = true)]
    public class Users : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        public string Name { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        [Required,EmailAddress]
        public string Email { get; set; }


        [Column(TypeName = "VARCHAR(255)")]
        [Required, MaxLength(255)]

        public string Password { get; set; }

        [Required, ForeignKey("Roles")]
        public int Role_Id { get; set; }

        public DateTime ? Created_At { get; set; } = DateTime.UtcNow.AddHours(5).AddMinutes(30);


        public Roles Roles { get; set; }
        public ICollection<Tickets> Tickets { get; set; }

        public ICollection<TicketComments> TicketComments { get; set; }

        public ICollection<TicketStatusLogs> TicketStatusLogs { get; set; }

    }
}