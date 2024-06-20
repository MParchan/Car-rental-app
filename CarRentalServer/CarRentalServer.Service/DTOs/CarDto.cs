using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.DTOs
{
    public class CarDto
    {
        public int CarId { get; set; }

        [Required]
        public int ModelId { get; set; }

        [Required]
        [Range(0.01, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public decimal PricePerDay { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int ProductionYear { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int Horsepower { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int Range { get; set; }

        public virtual ModelDto Model { get; set; }
    }
}
