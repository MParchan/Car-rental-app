using AutoMapper;
using CarRentalServer.Repository.Entities;
using CarRentalServer.Repository.Repositories;
using CarRentalServer.Service.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.LocationServis
{
    public class LocationServis: ILocationServis
    {
        private readonly IGenericRepository<Location> _locationRepository;
        private readonly IMapper _mapper;
        public LocationServis(IGenericRepository<Location> locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<LocationDto>> GetAllLocationsAsync()
        {
            return _mapper.Map<List<LocationDto>>(await _locationRepository.GetAllAsync());
        }

        public async Task<LocationDto> GetLocationByIdAsync(int id)
        {
            var location = await _locationRepository.GetByIdAsync(id);
            if (location == null)
            {
                throw new KeyNotFoundException("Location not found");
            }
            return _mapper.Map<LocationDto>(location);
        }

        public async Task<LocationDto> AddLocationAsync(LocationDto location)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(location, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(location, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            var locationEntity = _mapper.Map<Location>(location);
            await _locationRepository.AddAsync(locationEntity);
            return await GetLocationByIdAsync(locationEntity.LocationId);
        }

        public async Task UpdateLocationAsync(LocationDto location)
        {
            var existingLocation = await _locationRepository.GetByIdAsync(location.LocationId);
            if (existingLocation == null)
            {
                throw new KeyNotFoundException("Location not found");
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(location, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(location, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            await _locationRepository.UpdateAsync(_mapper.Map(location, existingLocation));
        }

        public async Task DeleteLocationAsync(int id)
        {
            var location = await _locationRepository.GetByIdAsync(id);
            if (location == null)
            {
                throw new KeyNotFoundException("Location not found");
            }
            await _locationRepository.DeleteAsync(location);
        }
    }
}
