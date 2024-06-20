using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.ViewModels
{
    public class LocationCarViewModelGet
    {
        public int LocationCarId { get; set; }
        public int LocationId { get; set; }
        public int CarId { get; set; }
        public int Quantity { get; set; }

        public virtual LocationViewModelGet Location { get; set; }
        public virtual CarViewModelGet Car { get; set; }
    }

    public class LocationCarViewModelPost
    {
        [Required(ErrorMessage = "Location car {0} is required.")]
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Location car {0} is required.")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Location car {0} is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Value for {0} cannot be negative.")]
        public int Quantity { get; set; }
    }

    public class LocationCarViewModelPut
    {
        [Required(ErrorMessage = "Location car {0} is required.")]
        public int LocationCarId { get; set; }

        [Required(ErrorMessage = "Location car {0} is required.")]
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Location car {0} is required.")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Location car {0} is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Value for {0} cannot be negative.")]
        public int Quantity { get; set; }
    }
}
