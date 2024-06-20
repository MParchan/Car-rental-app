using CarRentalServer.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.LocationCarServis
{
    public interface ILocationCarServis
    {
        Task<IEnumerable<LocationCarDto>> GetAllLocationCarsAsync();
        Task<LocationCarDto> GetLocationCarByIdAsync(int id);
        Task<LocationCarDto> AddLocationCarAsync(LocationCarDto locationCar);
        Task UpdateLocationCarAsync(LocationCarDto locationCar);
        Task DeleteLocationCarAsync(int id);
    }
}
