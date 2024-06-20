using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Repository.Entities
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        public int ModelId { get; set; }
        [Precision(18, 2)]
        public decimal PricePerDay { get; set; }
        public int ProductionYear { get; set; }
        public int Horsepower { get; set; }
        public int Range { get; set; }
        
        public virtual Model Model { get; set; }
    }
}
