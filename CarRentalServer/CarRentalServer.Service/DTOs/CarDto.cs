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
        [Key]
        public int CarId { get; set; }

        [Required]
        public int CarTypeId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Field {0} must be up to {1} characters long.")]
        public string Model { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Value for {0} must have up to two decimal places.")]
        public decimal PricePerDay { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int ProductionYear { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int Horsepower { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int SeatsNumber { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int Range { get; set; }

        public virtual CarTypeDto CarType { get; set; }
    }
}
