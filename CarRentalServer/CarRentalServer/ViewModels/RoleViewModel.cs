using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.ViewModels
{
    public class RoleViewModelGet
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
    }

    public class RoleViewModelPost
    {
        [Required]
        [StringLength(30, ErrorMessage = "Field {0} must be up to {1} characters long.")]
        public string Name { get; set; }
    }

    public class RoleViewModelPut
    {
        public int RoleId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Field {0} must be up to {1} characters long.")]
        public string Name { get; set; }
    }
}
