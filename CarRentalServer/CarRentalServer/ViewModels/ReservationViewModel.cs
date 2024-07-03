using CarRentalServer.Repository.Entities;
using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.ViewModels
{
    public class ReservationViewModelGet
    {
        public int ReservationId { get; set; }
        public int CarId { get; set; }
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
        public int RentalLocationId { get; set; }
        public int ReturnLocationId { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public decimal RentPrice { get; set; }
        public ReservationStatus Status { get; set; }
        public CarViewModelGet Car { get; set; }
        public LocationViewModelGet RentalLocation { get; set; }
        public LocationViewModelGet ReturnLocation { get; set; }
    }

    public class PagedReservationViewModelGet
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public ICollection<ReservationViewModelGet> Data { get; set; }
    }

    public class ReservationViewModelPost
    {

        [Required(ErrorMessage = "Reservation {0} is required.")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Reservation {0} is required.")]
        public int RentalLocationId { get; set; }

        [Required(ErrorMessage = "Reservation {0} is required.")]
        public int ReturnLocationId { get; set; }

        [Required(ErrorMessage = "Reservation {0} is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Reservation {0} is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
    }

    public class ReservationViewModelPut
    {
        [Required(ErrorMessage = "Reservation {0} is required.")]
        public int ReservationId { get; set; }

        [Required(ErrorMessage = "Reservation {0} is required.")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Reservation {0} is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Reservation {0} is required.")]
        public int RentalLocationId { get; set; }

        [Required(ErrorMessage = "Reservation {0} is required.")]
        public int ReturnLocationId { get; set; }

        [Required(ErrorMessage = "Reservation {0} is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Reservation {0} is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
    }
}
