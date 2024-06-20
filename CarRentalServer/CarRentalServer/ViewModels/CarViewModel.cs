using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.ViewModels
{
    public class CarViewModelGet
    {
        public int CarId { get; set; }
        public int ModelId { get; set; }
        public decimal PricePerDay { get; set; }
        public int ProductionYear { get; set; }
        public int Horsepower { get; set; }
        public int Range { get; set; }
        public ModelViewModelGet Model { get; set; }
    }

    public class CarViewModelPost
    {
        [Required(ErrorMessage = "Car {0} is required.")]
        public int ModelId { get; set; }

        [Required(ErrorMessage = "Car {0} is required.")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public decimal PricePerDay { get; set; }

        [Required(ErrorMessage = "Car {0} is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int ProductionYear { get; set; }

        [Required(ErrorMessage = "Car {0} is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int Horsepower { get; set; }

        [Required(ErrorMessage = "Car {0} is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int Range { get; set; }
    }

    public class CarViewModelPut
    {
        [Required(ErrorMessage = "Car {0} is required.")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Car {0} is required.")]
        public int ModelId { get; set; }

        [Required(ErrorMessage = "Car {0} is required.")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public decimal PricePerDay { get; set; }

        [Required(ErrorMessage = "Car {0} is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int ProductionYear { get; set; }

        [Required(ErrorMessage = "Car {0} is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int Horsepower { get; set; }

        [Required(ErrorMessage = "Car {0} is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int Range { get; set; }
    }
}
