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
            return await _context.Set<Car>().Include(i => i.CarType).ToListAsync();
        }
        public async Task<Car> GetByIdWithIncludesAsync(int id)
        {
            return await _context.Set<Car>().Include(car => car.CarType).FirstOrDefaultAsync(car => car.CarId == id);
        }
    }
}
