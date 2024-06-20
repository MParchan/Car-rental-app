using AutoMapper;
using CarRentalServer.Repository.Entities;
using CarRentalServer.Repository.Repositories.LocationCarRepository;
using CarRentalServer.Service.DTOs;
using CarRentalServer.Service.Services.CarService;
using CarRentalServer.Service.Services.LocationServis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.LocationCarServis
{
    public class LocationCarService: ILocationCarServis
    {
        private readonly ILocationCarRepository _locationCarRepository;
        private readonly ILocationServis _locationService;
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        public LocationCarService(ILocationCarRepository locationCarRepository, ILocationServis locationService, ICarService carService,  IMapper mapper)
        {
            _locationCarRepository = locationCarRepository;
            _locationService = locationService;
            _carService = carService;
            _mapper = mapper;
        }


        public async Task<IEnumerable<LocationCarDto>> GetAllLocationCarsAsync()
        {
            return _mapper.Map<List<LocationCarDto>>(await _locationCarRepository.GetAllWithIncludesAsync());
        }
        public async Task<LocationCarDto> GetLocationCarByIdAsync(int id)
        {
            var locationCar = await _locationCarRepository.GetByIdWithIncludesAsync(id);
            if (locationCar == null)
            {
                throw new KeyNotFoundException("Location car not found");
            }

            return _mapper.Map<LocationCarDto>(locationCar);
        }

        public async Task<LocationCarDto> AddLocationCarAsync(LocationCarDto locationCar)
        {
            try
            {
                await _locationService.GetLocationByIdAsync(locationCar.LocationId);
                await _carService.GetCarByIdAsync(locationCar.CarId);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(locationCar, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(locationCar, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            var locationCarEntity = _mapper.Map<LocationCar>(locationCar);
            await _locationCarRepository.AddAsync(locationCarEntity);
            return await GetLocationCarByIdAsync(locationCarEntity.LocationCarId);
        }

        public async Task UpdateLocationCarAsync(LocationCarDto locationCar)
        {
            var existingLocationCar = await _locationCarRepository.GetByIdNoTrackingAsync(locationCar.LocationCarId);
            if (existingLocationCar == null)
            {
                throw new KeyNotFoundException("Location car not found");
            }

            try
            {
                await _locationService.GetLocationByIdAsync(locationCar.LocationId);
                await _carService.GetCarByIdAsync(locationCar.CarId);
            }
            catch
            {
                throw;
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(locationCar, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(locationCar, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            await _locationCarRepository.UpdateAsync(_mapper.Map<LocationCar>(locationCar));
        }

        public async Task DeleteLocationCarAsync(int id)
        {
            var locationCar = await _locationCarRepository.GetByIdAsync(id);
            if (locationCar == null)
            {
                throw new KeyNotFoundException("Location car not found");
            }
            await _locationCarRepository.DeleteAsync(locationCar);
        }
    }
}
