﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.DTOs
{
    public class ModelDto
    {
        public int ModelId { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public int CarTypeId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Field {0} must be up to {1} characters long.")]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int SeatsNumber { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public virtual BrandDto Brand { get; set; }
        public virtual CarTypeDto CarType { get; set; }
        public virtual ICollection<CarDto> Cars { get; set; }
    }
}
