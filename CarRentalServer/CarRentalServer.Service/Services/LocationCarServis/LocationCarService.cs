using AutoMapper;
using CarRentalServer.Repository.Entities;
using CarRentalServer.Repository.Repositories.LocationCarRepository;
using CarRentalServer.Service.DTOs;
using CarRentalServer.Service.Services.CarService;
using CarRentalServer.Service.Services.LocationServis;
using CarRentalServer.Service.Services.ModelService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.LocationCarServis
{
    public class LocationCarService: ILocationCarService
    {
        private readonly ILocationCarRepository _locationCarRepository;
        private readonly ILocationServis _locationService;
        private readonly ICarService _carService;
        private readonly IModelService _modelService;
        private readonly IMapper _mapper;
        public LocationCarService(ILocationCarRepository locationCarRepository, ILocationServis locationService, ICarService carService, IModelService modelService, IMapper mapper)
        {
            _locationCarRepository = locationCarRepository;
            _locationService = locationService;
            _carService = carService;
            _modelService = modelService;
            _mapper = mapper;
        }


        public async Task<IEnumerable<LocationCarDto>> GetAllLocationCarsAsync()
        {
            return _mapper.Map<List<LocationCarDto>>(await _locationCarRepository.GetAllWithIncludesAsync());
        }

        public async Task<IEnumerable<LocationCarDto>> GetLocationCarsByLocationIdAndModelId(int locationId, int modelId )
        {
            try
            {
                await _modelService.GetModelByIdAsync(modelId);
                await _locationService.GetLocationByIdAsync(locationId);
            }
            catch
            {
                throw;
            }

            var locationCars = await _locationCarRepository.GetAllByLocationAndModelAsync(locationId, modelId);
            return _mapper.Map<List<LocationCarDto>>(locationCars);
        }

        public async Task<LocationCarDto> GetLocationCarByIdAsync(int id)
        {
            var locationCar = await _locationCarRepository.GetByIdWithIncludesAsync(id);
            if (locationCar == null)
            {
                throw new KeyNotFoundException("Location car not found.");
            }

            return _mapper.Map<LocationCarDto>(locationCar);
        }

        public async Task<LocationCarDto> GetLocationCarByLocationIdAndCarIdAsync(int locationId, int carId)
        {
            try
            {
                await _locationService.GetLocationByIdAsync(locationId);
                await _carService.GetCarByIdAsync(carId);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            var locationCar = await _locationCarRepository.GetByLocationIdAndCarIdAsync(locationId, carId);
            if (locationCar == null)
            {
                throw new KeyNotFoundException("Location car not found.");
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
                throw new KeyNotFoundException("Location car not found.");
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
                throw new KeyNotFoundException("Location car not found.");
            }
            await _locationCarRepository.DeleteAsync(locationCar);
        }

        public async Task RentCarAsync(int locationId, int carId)
        {
            try
            {
                var locationCar = await GetLocationCarByLocationIdAndCarIdAsync(locationId, carId);
                if (locationCar.Quantity == 0)
                {
                    throw new ValidationException("Car is not available in this location.");
                }
                locationCar.Quantity--;
                await UpdateLocationCarAsync(locationCar);
            }
            catch 
            {
                throw;
            }
        }

        public async Task ReturnCarAsync(int locationId, int carId)
        {
            var locationCar = await _locationCarRepository.GetByLocationIdAndCarIdAsync(locationId, carId);
            try
            {
                if (locationCar == null)
                {
                    LocationCarDto newLocationCar = new() { LocationId = locationId, CarId = carId, Quantity = 1 };
                    await AddLocationCarAsync(newLocationCar);
                }
                else
                {
                    locationCar.Quantity++;
                    await UpdateLocationCarAsync(_mapper.Map<LocationCarDto>(locationCar));
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
