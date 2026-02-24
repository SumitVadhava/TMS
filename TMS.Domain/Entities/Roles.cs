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
    [Index(nameof(Name), IsUnique = true)]
    public class Roles : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [EnumDataType(typeof(RolesName), ErrorMessage = "Invalid Role value")]
        public RolesName Name { get; set; } // Enum che + uper index che aetle unique value aavshe

    }
}