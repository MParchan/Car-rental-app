using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.ViewModels
{
    public class LocationViewModelGet
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
    }

    public class LocationViewModelPost
    {
        [Required(ErrorMessage = "Location {0} is required.")]
        [StringLength(30, ErrorMessage = "Field {0} must be up to {1} characters long.")]
        public string Name { get; set; }
    }

    public class LocationViewModelPut
    {
        [Required(ErrorMessage = "Location {0} is required.")]
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Location {0} is required.")]
        [StringLength(30, ErrorMessage = "Field {0} must be up to {1} characters long.")]
        public string Name { get; set; }
    }
}
