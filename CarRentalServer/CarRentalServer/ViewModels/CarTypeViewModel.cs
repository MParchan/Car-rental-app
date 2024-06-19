using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.ViewModels
{
    public class CarTypeViewModel
    {
        [Key]
        public int CarTypeId { get; set; }

        [Required(ErrorMessage = "Car type {0} is required.")]
        [StringLength(30, ErrorMessage = "Field {0} must be up to {1} characters long.")]
        public string Name { get; set; }
    }
}
