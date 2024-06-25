using CarRentalServer.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Repository.Repositories.CarRepository
{
    public class CarRepository: GenericRepository<Car>, ICarRepository 
    {
        private readonly AppDbContext _context;
        public CarRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAllWithIncludesAsync()
        {
            return await _context.Cars
                .Include(car => car.Model)
                    .ThenInclude(model => model.Brand)
                .Include(car => car.Model)
                    .ThenInclude(model => model.CarType)
                .Select(car => new Car
                {
                    CarId = car.CarId,
                    ModelId = car.ModelId,
                    Version = car.Version,
                    PricePerDay = car.PricePerDay,
                    ProductionYear = car.ProductionYear,
                    Horsepower = car.Horsepower,
                    Range = car.Range,
                    Model = new Model
                    {
                        ModelId = car.Model.ModelId,
                        Name = car.Model.Name,
                        SeatsNumber = car.Model.SeatsNumber,
                        ImageUrl = car.Model.ImageUrl,
                        Brand = car.Model.Brand,
                        CarType = car.Model.CarType,
                    }
                })
                .ToListAsync();
        }
        public async Task<Car> GetByIdWithIncludesAsync(int id)
        {
            return await _context.Cars
                .Include(car => car.Model)
                    .ThenInclude(model => model.Brand)
                .Include(car => car.Model)
                    .ThenInclude(model => model.CarType)
                .Select(car => new Car
                {
                    CarId = car.CarId,
                    ModelId = car.ModelId,
                    Version = car.Version,
                    PricePerDay = car.PricePerDay,
                    ProductionYear = car.ProductionYear,
                    Horsepower = car.Horsepower,
                    Range = car.Range,
                    Model = new Model
                    {
                        ModelId = car.Model.ModelId,
                        Name = car.Model.Name,
                        SeatsNumber = car.Model.SeatsNumber,
                        ImageUrl = car.Model.ImageUrl,
                        Brand = car.Model.Brand,
                        CarType = car.Model.CarType,
                    }
                })
                .FirstOrDefaultAsync(car => car.CarId == id);
        }
        public async Task<Car> GetByIdNoTrackingAsync(int id)
        {
            return await _context.Cars
                .AsNoTracking()
                .FirstOrDefaultAsync(car => car.CarId == id);
        }
    }
}
