using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.DTOs
{
    public class LocationCarDto
    {
        public int LocationCarId { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value for {0} cannot be negative.")]
        public int Quantity { get; set; }

        public virtual LocationDto Location { get; set; }
        public virtual CarDto Car { get; set; }
    }
}
