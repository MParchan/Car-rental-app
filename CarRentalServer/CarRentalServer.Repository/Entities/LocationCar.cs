using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Repository.Entities
{
    public class LocationCar
    {
        [Key]
        public int LocationCarId { get; set; }
        public int LocationId { get; set; }
        public int CarId { get; set; }
        public int Quantity { get; set; }

        public virtual Location Location { get; set; }
        public virtual Car Car { get; set; }
    }
}
