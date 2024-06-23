using CarRentalServer.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Repository.Repositories.LocationCarRepository
{
    public class LocationCarRepository: GenericRepository<LocationCar>, ILocationCarRepository
    {
        private readonly AppDbContext _context;
        public LocationCarRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LocationCar>> GetAllWithIncludesAsync()
        {
            return await _context.LocationCars
                .Include(locationCar => locationCar.Car)
                .Include(locationCar => locationCar.Location).ToListAsync();
        }
        public async Task<LocationCar> GetByIdWithIncludesAsync(int id)
        {
            return await _context.LocationCars
                .Include(locationCar => locationCar.Car)
                .Include(locationCar => locationCar.Location)
                .FirstOrDefaultAsync(locationCar => locationCar.LocationCarId == id);
        }
        public async Task<LocationCar> GetByIdNoTrackingAsync(int id)
        {
            return await _context.LocationCars
                .AsNoTracking()
                .FirstOrDefaultAsync(locationCar => locationCar.LocationCarId == id);
        }
        public async Task<LocationCar> GetByLocationIdAndCarIdAsync(int locationId, int carId)
        {
            return await _context.LocationCars
                .AsNoTracking()
                .FirstOrDefaultAsync(lc => lc.LocationId == locationId && lc.CarId == carId);
        }
    }
}
