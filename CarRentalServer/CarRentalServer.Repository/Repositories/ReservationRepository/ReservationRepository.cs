using CarRentalServer.Repository.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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

        public async Task<IEnumerable<Reservation>> GetAllWithIncludesAsync(int pageNumber, int pageSize, string sortField, string sortOrder)
        {
            var query = _context.Reservations
                 .Include(r => r.Car)
                     .ThenInclude(c => c.Model)
                         .ThenInclude(m => m.Brand)
                 .Include(r => r.Car)
                     .ThenInclude(c => c.Model)
                         .ThenInclude(m => m.CarType)
                 .Include(r => r.RentalLocation)
                 .Include(r => r.ReturnLocation)
                 .AsQueryable();

            if (!string.IsNullOrEmpty(sortField))
            {
                try
                {
                    query = sortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(e => EF.Property<object>(e, sortField))
                        : query.OrderBy(e => EF.Property<object>(e, sortField));
                }
                catch (ArgumentException)
                {
                    query = query.OrderBy(e => e.ReservationId);
                }
            }
            else
            {
                query = query.OrderBy(e => e.ReservationId);
            }

            return await query
                .Select(r => new Reservation
                {
                    ReservationId = r.ReservationId,
                    CarId = r.CarId,
                    UserEmail = r.UserEmail,
                    RentalLocationId = r.RentalLocationId,
                    ReturnLocationId = r.ReturnLocationId,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    RentPrice = r.RentPrice,
                    Status = r.Status,
                    RentalLocation = r.RentalLocation,
                    ReturnLocation = r.ReturnLocation,
                    Car = new Car
                    {
                        CarId = r.Car.CarId,
                        ModelId = r.Car.ModelId,
                        Version = r.Car.Version,
                        PricePerDay = r.Car.PricePerDay,
                        ProductionYear = r.Car.ProductionYear,
                        Horsepower = r.Car.Horsepower,
                        Range = r.Car.Range,
                        Model = new Model
                        {
                            ModelId = r.Car.Model.ModelId,
                            Name = r.Car.Model.Name,
                            SeatsNumber = r.Car.Model.SeatsNumber,
                            ImageUrl = r.Car.Model.ImageUrl,
                            Brand = r.Car.Model.Brand,
                            CarType = r.Car.Model.CarType,
                        }
                    }
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllUserReservationsWithIncludesAsync(string userEmail, int pageNumber, int pageSize, string sortField, string sortOrder)
        {
            var query = _context.Reservations
                 .Include(r => r.Car)
                     .ThenInclude(c => c.Model)
                         .ThenInclude(m => m.Brand)
                 .Include(r => r.Car)
                     .ThenInclude(c => c.Model)
                         .ThenInclude(m => m.CarType)
                 .Include(r => r.RentalLocation)
                 .Include(r => r.ReturnLocation)
                 .Where(r => r.UserEmail.Equals(userEmail));

            if (!string.IsNullOrEmpty(sortField))
            {
                try
                {
                    query = sortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(e => EF.Property<object>(e, sortField))
                        : query.OrderBy(e => EF.Property<object>(e, sortField));
                }
                catch (ArgumentException)
                {
                    query = query.OrderBy(e => e.ReservationId);
                }
            }
            else
            {
                query = query.OrderBy(e => e.ReservationId);
            }

            return await query
                .Select(r => new Reservation
                {
                    ReservationId = r.ReservationId,
                    CarId = r.CarId,
                    UserEmail = r.UserEmail,
                    RentalLocationId = r.RentalLocationId,
                    ReturnLocationId = r.ReturnLocationId,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    RentPrice = r.RentPrice,
                    Status = r.Status,
                    RentalLocation = r.RentalLocation,
                    ReturnLocation = r.ReturnLocation,
                    Car = new Car
                    {
                        CarId = r.Car.CarId,
                        ModelId = r.Car.ModelId,
                        Version = r.Car.Version,
                        PricePerDay = r.Car.PricePerDay,
                        ProductionYear = r.Car.ProductionYear,
                        Horsepower = r.Car.Horsepower,
                        Range = r.Car.Range,
                        Model = new Model
                        {
                            ModelId = r.Car.Model.ModelId,
                            Name = r.Car.Model.Name,
                            SeatsNumber = r.Car.Model.SeatsNumber,
                            ImageUrl = r.Car.Model.ImageUrl,
                            Brand = r.Car.Model.Brand,
                            CarType = r.Car.Model.CarType,
                        }
                    }
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
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
                .Select(r => new Reservation
                {
                    ReservationId = r.ReservationId,
                    CarId = r.CarId,
                    UserEmail = r.UserEmail,
                    RentalLocationId = r.RentalLocationId,
                    ReturnLocationId = r.ReturnLocationId,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    RentPrice = r.RentPrice,
                    Status = r.Status,
                    RentalLocation = r.RentalLocation,
                    ReturnLocation = r.ReturnLocation,
                    Car = new Car
                    {
                        CarId = r.Car.CarId,
                        ModelId = r.Car.ModelId,
                        Version = r.Car.Version,
                        PricePerDay = r.Car.PricePerDay,
                        ProductionYear = r.Car.ProductionYear,
                        Horsepower = r.Car.Horsepower,
                        Range = r.Car.Range,
                        Model = new Model
                        {
                            ModelId = r.Car.Model.ModelId,
                            Name = r.Car.Model.Name,
                            SeatsNumber = r.Car.Model.SeatsNumber,
                            ImageUrl = r.Car.Model.ImageUrl,
                            Brand = r.Car.Model.Brand,
                            CarType = r.Car.Model.CarType,
                        }
                    }
                })
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

        public async Task<int> GetTotalReservationCountAsync()
        {
            return await _context.Reservations.CountAsync();
        }
        public async Task<int> GetUserReservationsCountAsync(string userEmail)
        {
            return await _context.Reservations
                .Where(r => r.UserEmail == userEmail)
                .CountAsync();
        }
    }
}
