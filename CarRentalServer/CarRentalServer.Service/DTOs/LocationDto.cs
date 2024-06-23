using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.DTOs
{
    public class LocationDto
    {
        public int LocationId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Field {0} must be up to {1} characters long.")]
        public string Name { get; set; }
    }
}
