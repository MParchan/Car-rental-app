﻿using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.ViewModels
{
    public class CarViewModelGet
    {
        public int CarId { get; set; }
        public int CarTypeId { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public int ProductionYear { get; set; }
        public int Horsepower { get; set; }
        public int SeatsNumber { get; set; }
        public int Range { get; set; }
        public CarTypeViewModelGet CarType { get; set; }
    }

    public class CarViewModelPost
    {
        [Required]
        public int CarTypeId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Field {0} must be up to {1} characters long.")]
        public string Model { get; set; }

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
        public int SeatsNumber { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int Range { get; set; }
    }

    public class CarViewModelPut
    {
        [Required]
        public int CarId { get; set; }

        [Required]
        public int CarTypeId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Field {0} must be up to {1} characters long.")]
        public string Model { get; set; }

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
        public int SeatsNumber { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int Range { get; set; }
    }
}