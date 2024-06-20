using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.ViewModels
{
    public class BrandViewModelGet
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
    }

    public class BrandViewModelPost
    {
        [Required(ErrorMessage = "Brand {0} is required.")]
        [StringLength(30, ErrorMessage = "Field {0} must be up to {1} characters long.")]
        public string Name { get; set; }
    }

    public class BrandViewModelPut
    {
        [Required(ErrorMessage = "Brand {0} is required.")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Brand {0} is required.")]
        [StringLength(30, ErrorMessage = "Field {0} must be up to {1} characters long.")]
        public string Name { get; set; }
    }
}
