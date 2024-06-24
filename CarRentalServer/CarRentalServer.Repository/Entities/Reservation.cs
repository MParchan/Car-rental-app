using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Repository.Entities
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public int CarId { get; set; }
        public string UserEmail { get; set; }
        public int RentalLocationId { get; set; }
        public int ReturnLocationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Precision(18, 2)]
        public decimal RentPrice { get; set; }
        public ReservationStatus Status { get; set; }

        public virtual Car Car { get; set; }
        public virtual Location RentalLocation { get; set; }
        public virtual Location ReturnLocation { get; set; }
    }

    public enum ReservationStatus
    {
        Pending,
        Started,
        Cancelled,
        Completed
    }
}
