using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Repository.Entities
{
    public class Car
    {
        public int CarId { get; set; }
        public int CarTypeId { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public int ProductionYear { get; set; }
        public int Horsepower { get; set; }
        public int SeatsNumber { get; set; }
        public int Range { get; set; }
        
        public virtual CarType CarType { get; set; }
    }
}
