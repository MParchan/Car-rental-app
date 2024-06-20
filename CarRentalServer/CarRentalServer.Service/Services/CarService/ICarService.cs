using CarRentalServer.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.CarService
{
    public interface ICarService
    {
        Task<IEnumerable<CarDto>> GetAllCarsAsync();
        Task<CarDto> GetCarByIdAsync(int id);
        Task<CarDto> AddCarAsync(CarDto car);
        Task UpdateCarAsync(CarDto car);
        Task DeleteCarAsync(int id);
    }
}
