using CarRentalServer.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Repository.Repositories.ReservationRepository
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        private readonly AppDbContext _context;
        public ReservationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllWithIncludesAsync()
        {
            return await _context.Reservations
                .Include(r => r.Car)
                    .ThenInclude(c => c.Model)
                        .ThenInclude(m => m.Brand)
                .Include(r => r.Car)
                    .ThenInclude(c => c.Model)
                        .ThenInclude(m => m.CarType)
                .Include(r => r.RentalLocation)
                .Include(r => r.ReturnLocation)
                .ToListAsync();
        }
        public async Task<IEnumerable<Reservation>> GetAllUserReservationsWithIncludesAsync(string userEmail)
        {
            return await _context.Reservations
                .Include(r => r.Car)
                    .ThenInclude(c => c.Model)
                        .ThenInclude(m => m.Brand)
                .Include(r => r.Car)
                    .ThenInclude(c => c.Model)
                        .ThenInclude(m => m.CarType)
                .Include(r => r.RentalLocation)
                .Include(r => r.ReturnLocation)
                .Where(r => r.UserEmail.Equals(userEmail))
                .ToListAsync();
        }
        public async Task<Reservation> GetByIdWithIncludesAsync(int id)
        {
            return await _context.Reservations
                .Include(r => r.Car)
                    .ThenInclude(c => c.Model)
                        .ThenInclude(m => m.Brand)
                .Include(r => r.Car)
                    .ThenInclude(c => c.Model)
                        .ThenInclude(m => m.CarType)
                .Include(r => r.RentalLocation)
                .Include(r => r.ReturnLocation)
                .FirstOrDefaultAsync(r => r.ReservationId == id);
        }
        public async Task<Reservation> GetByIdNoTrackingAsync(int id)
        {
            return await _context.Reservations
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ReservationId == id);
        }
        public async Task<int> NumberOfPendingReservations(int locationId, int carId, DateTime startDate, DateTime endDate)
        {
            return await _context.Reservations
                .Where(r => r.RentalLocationId == locationId && r.CarId == carId &&
                            (r.StartDate <= endDate && r.EndDate >= startDate) &&
                            r.Status == ReservationStatus.Pending)
                .CountAsync();
        }
    }
}
