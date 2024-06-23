using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Repository.Entities
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string Name { get; set; }
    }
}
