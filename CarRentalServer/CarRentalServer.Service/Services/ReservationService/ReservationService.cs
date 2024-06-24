using AutoMapper;
using CarRentalServer.Repository.Entities;
using CarRentalServer.Repository.Repositories.ReservationRepository;
using CarRentalServer.Repository.Repositories.UserRepository;
using CarRentalServer.Service.DTOs;
using CarRentalServer.Service.Services.CarService;
using CarRentalServer.Service.Services.LocationCarServis;
using CarRentalServer.Service.Services.LocationServis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICarService _carService;
        private readonly ILocationServis _locationService;
        private readonly ILocationCarServis _locationCarServis;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public ReservationService(IReservationRepository reservationRepository, ICarService carService, ILocationServis locationServis, ILocationCarServis locationCarServis, IUserRepository userRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _carService = carService;
            _locationService = locationServis;
            _locationCarServis = locationCarServis;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync(string userEmail)
        {
            var user = await _userRepository.GetUserByEmailAsync(userEmail);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            if (user.Role.Name.Equals("Admin") || user.Role.Name.Equals("Manager"))
            {
                return _mapper.Map<List<ReservationDto>>(await _reservationRepository.GetAllWithIncludesAsync());
            }
            return _mapper.Map<List<ReservationDto>>(await _reservationRepository.GetAllUserReservationsWithIncludesAsync(userEmail));
        }

        public async Task<ReservationDto> GetReservationByIdAsync(string userEmail, int id)
        {
            var user = await _userRepository.GetUserByEmailAsync(userEmail);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            var reservation = await _reservationRepository.GetByIdWithIncludesAsync(id);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }
            if (!user.Role.Name.Equals("Admin") && !user.Role.Name.Equals("Manager") && !userEmail.Equals(reservation.UserEmail))
            {
                throw new UnauthorizedAccessException();
            }

            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<ReservationDto> AddReservationAsync(string userEmail, ReservationDto reservation)
        {
            var user = await _userRepository.GetUserByEmailAsync(userEmail);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            decimal pricePerDay = 0;
            try
            {
                await _locationService.GetLocationByIdAsync(reservation.RentalLocationId);
                await _locationService.GetLocationByIdAsync(reservation.ReturnLocationId);
                var car = await _carService.GetCarByIdAsync(reservation.CarId);
                pricePerDay = car.PricePerDay;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }              

            if(reservation.StartDate < DateTime.Now.Date)
            {
                throw new ValidationException("You can't make reservations backwards.");
            }
            if (reservation.EndDate < reservation.StartDate)
            {
                throw new ValidationException("The end of the reservation cannot be earlier than the start.");
            }
            var numberOfReservedCars = await _reservationRepository.NumberOfPendingReservations(reservation.RentalLocationId, reservation.CarId, reservation.StartDate, reservation.EndDate);
            var locationCar = await _locationCarServis.GetLocationCarByLocationIdAndCarIdAsync(reservation.RentalLocationId, reservation.CarId);
            if (locationCar.Quantity - numberOfReservedCars <= 0)
            {
                throw new ValidationException("The car is not available at the selected location and date.");
            }

            TimeSpan rentDays = reservation.EndDate - reservation.StartDate;
            reservation.RentPrice = (1 + rentDays.Days) * pricePerDay;
            if (1 + rentDays.Days >= 5)
            {
                reservation.RentPrice *= 0.8m;
            }
            if (1 + rentDays.Days >= 3)
            {
                reservation.RentPrice *= 0.9m;
            }
            reservation.RentPrice = Math.Round(reservation.RentPrice, 2);
            reservation.UserEmail = userEmail;

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(reservation, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(reservation, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            var reservationEntity = _mapper.Map<Reservation>(reservation);
            await _reservationRepository.AddAsync(reservationEntity);
            return await GetReservationByIdAsync(reservationEntity.UserEmail, reservationEntity.ReservationId);
        }

        public async Task UpdateReservationAsync(ReservationDto reservation)
        {
            var existingReservation = await _reservationRepository.GetByIdNoTrackingAsync(reservation.ReservationId);
            if (existingReservation == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }

            var user = await _userRepository.GetUserByEmailAsync(reservation.UserEmail);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            decimal pricePerDay = 0;
            try
            {
                await _locationService.GetLocationByIdAsync(reservation.RentalLocationId);
                await _locationService.GetLocationByIdAsync(reservation.ReturnLocationId);
                var car = await _carService.GetCarByIdAsync(reservation.CarId);
                pricePerDay = car.PricePerDay;
            }
            catch
            {
                throw;
            }

            if (reservation.StartDate < DateTime.Now.Date)
            {
                throw new ValidationException("You can't make reservations backwards.");
            }
            if (reservation.EndDate < reservation.StartDate)
            {
                throw new ValidationException("The end of the reservation cannot be earlier than the start.");
            }
            TimeSpan rentDays = reservation.EndDate - reservation.StartDate;
            reservation.RentPrice = (1 + rentDays.Days) * pricePerDay;
            if(1 + rentDays.Days >= 5)
            {
                reservation.RentPrice *= 0.8m;
            }
            if (1 + rentDays.Days >= 3)
            {
                reservation.RentPrice *= 0.9m;
            }
            reservation.RentPrice = Math.Round(reservation.RentPrice, 2);

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(reservation, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(reservation, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            await _reservationRepository.UpdateAsync(_mapper.Map<Reservation>(reservation));
        }

        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }
            await _reservationRepository.DeleteAsync(reservation);
        }

        public async Task StartReservationAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdNoTrackingAsync(id);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }
            if(DateTime.Now.Date < reservation.StartDate)
            {
                throw new ValidationException("You cannot rent a car before the start date in your reservation.");
            }
            if (reservation.EndDate < DateTime.Now.Date)
            {
                throw new ValidationException("You cannot rent a car after the end date in your reservation.");
            }
            if (reservation.Status != ReservationStatus.Pending)
            {
                throw new ValidationException("The status cannot be changed to Started because the reservation is not in Pending status.");
            }
            try
            {
                await _locationCarServis.RentCarAsync(reservation.RentalLocationId, reservation.CarId);
                reservation.Status = ReservationStatus.Started;
                await UpdateReservationAsync(_mapper.Map<ReservationDto>(reservation));
            }
            catch
            {
                throw;
            }
        }

        public async Task EndReservationAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdNoTrackingAsync(id);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }
            if (reservation.Status != ReservationStatus.Started)
            {
                throw new ValidationException("The status cannot be changed to Completed because the reservation is not in Started status.");
            }
            try
            {
                await _locationCarServis.ReturnCarAsync(reservation.ReturnLocationId, reservation.CarId);
                reservation.Status = ReservationStatus.Completed;
                await UpdateReservationAsync(_mapper.Map<ReservationDto>(reservation));
            }
            catch
            {
                throw;
            }
        }

        public async Task CancelReservationAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdNoTrackingAsync(id);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }
            if (reservation.Status == ReservationStatus.Started)
            {
                throw new ValidationException("The reservation cannot be canceled because it has already started.");
            }
            try
            {
                reservation.Status = ReservationStatus.Cancelled;
                await UpdateReservationAsync(_mapper.Map<ReservationDto>(reservation));
            }
            catch
            {
                throw;
            }
        }
    }
}
