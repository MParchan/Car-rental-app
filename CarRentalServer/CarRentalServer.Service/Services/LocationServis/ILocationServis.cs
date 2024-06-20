using CarRentalServer.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.LocationServis
{
    public interface ILocationServis
    {
        Task<IEnumerable<LocationDto>> GetAllLocationsAsync();
        Task<LocationDto> GetLocationByIdAsync(int id);
        Task<LocationDto> AddLocationAsync(LocationDto location);
        Task UpdateLocationAsync(LocationDto location);
        Task DeleteLocationAsync(int id);
    }
}
