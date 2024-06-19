using CarRentalServer.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.CarTypeService
{
    public interface ICarTypeService
    {
        Task<IEnumerable<CarTypeDto>> GetAllCarTypesAsync();
        Task<CarTypeDto> GetCarTypeByIdAsync(int id);
        Task AddCarTypeAsync(CarTypeDto carType);
        Task UpdateCarTypeAsync(CarTypeDto carType);
        Task DeleteCarTypeAsync(int id);
    }
}
