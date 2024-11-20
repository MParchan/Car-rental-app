using CarRentalServer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Repository.Repositories.ReservationRepository
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetAllWithIncludesAsync(int pageNumber, int pageSize, string sortField, string sortOrder);
        Task<IEnumerable<Reservation>> GetAllUserReservationsWithIncludesAsync(string userEmail, int pageNumber, int pageSize, string sortField, string sortOrder);
        Task<Reservation> GetByIdWithIncludesAsync(int id);
        Task<Reservation> GetByIdNoTrackingAsync(int id);
        Task<int> NumberOfPendingReservations(int locationId, int carId, DateTime startDate, DateTime endDate);
        Task<int> GetTotalReservationCountAsync();
        Task<int> GetUserReservationsCountAsync(string userEmail);
    }
}
