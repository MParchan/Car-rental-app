using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.DTOs
{
    public class CarTypeDto
    {
        [Key]
        public int CarTypeId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Field {0} must be up to {1} characters long.")]
        public string Name { get; set; }
    }
}
