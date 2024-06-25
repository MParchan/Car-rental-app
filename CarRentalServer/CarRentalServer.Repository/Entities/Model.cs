using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Repository.Entities
{
    public  class Model
    {
        [Key]
        public int ModelId { get; set; }
        public int BrandId { get; set; }
        public int CarTypeId { get; set; }
        public string Name { get; set; }
        public int SeatsNumber { get; set; }
        public string ImageUrl { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual CarType CarType { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
