using CarRentalServer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Repository.Repositories.CarRepository
{
    public interface ICarRepository: IGenericRepository<Car>
    {
        Task<IEnumerable<Car>> GetAllWithIncludesAsync();
        Task<Car> GetByIdWithIncludesAsync(int id);
        Task<Car> GetByIdNoTrackingAsync(int id);
    }
}
