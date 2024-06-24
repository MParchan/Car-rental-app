using CarRentalServer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.DTOs
{
    public class ReservationDto
    {
        public int ReservationId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public int RentalLocationId { get; set; }

        [Required]
        public int ReturnLocationId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(0.01, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public decimal RentPrice { get; set; }

        [Required]
        [DefaultValue(ReservationStatus.Pending)]
        public ReservationStatus Status { get; set; }

        public virtual CarDto Car { get; set; }
        public virtual LocationDto RentalLocation { get; set; }
        public virtual LocationDto ReturnLocation { get; set; }
    }
}
