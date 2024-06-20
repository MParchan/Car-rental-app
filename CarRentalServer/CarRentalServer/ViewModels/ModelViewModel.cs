using CarRentalServer.Repository.Entities;
using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.ViewModels
{
    public class ModelViewModelGet
    {
        public int ModelId { get; set; }
        public int BrandId { get; set; }
        public int CarTypeId { get; set; }
        public string Name { get; set; }
        public int SeatsNumber { get; set; }
        public Brand Brand { get; set; }
        public CarType CarType { get; set; }
    }

    public class ModelViewModelPost
    {

        [Required(ErrorMessage = "Model {0} is required.")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Model {0} is required.")]
        public int CarTypeId { get; set; }

        [Required(ErrorMessage = "Model {0} is required.")]
        [StringLength(50, ErrorMessage = "Field {0} must be up to {1} characters long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Model {0} is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int SeatsNumber { get; set; }
    }

    public class ModelViewModelPut
    {
        [Required(ErrorMessage = "Model {0} is required.")]
        public int ModelId { get; set; }

        [Required(ErrorMessage = "Model {0} is required.")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Model {0} is required.")]
        public int CarTypeId { get; set; }

        [Required(ErrorMessage = "Model {0} is required.")]
        [StringLength(50, ErrorMessage = "Field {0} must be up to {1} characters long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Model {0} is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int SeatsNumber { get; set; }
    }
}
