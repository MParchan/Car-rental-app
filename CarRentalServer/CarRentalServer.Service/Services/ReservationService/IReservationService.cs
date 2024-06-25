using CarRentalServer.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.ReservationService
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync(string userEmail);
        Task<IEnumerable<CarQuantityDto>> GetCarsQuantityWithoutPendingReservationAsync(IEnumerable<LocationCarDto> carsQuantity, DateTime startDate, DateTime endDate);
        Task<ReservationDto> GetReservationByIdAsync(string userEmail, int id);
        Task<ReservationDto> AddReservationAsync(string userEmail, ReservationDto reservation);
        Task UpdateReservationAsync(ReservationDto reservation);
        Task DeleteReservationAsync(int id);
        Task StartReservationAsync(int id);
        Task EndReservationAsync(int id);
        Task CancelReservationAsync(int id);
    }
}
