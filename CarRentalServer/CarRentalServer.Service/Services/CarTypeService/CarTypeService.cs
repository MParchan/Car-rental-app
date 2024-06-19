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

namespace CarRentalServer.Service.Services.CarTypeService
{
    public class CarTypeService : ICarTypeService
    {
        private readonly IGenericRepository<CarType> _carTypeRepository;
        private readonly IMapper _mapper;
        public CarTypeService(IGenericRepository<CarType> carTypeRepository, IMapper mapper)
        {
            _carTypeRepository = carTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarTypeDto>> GetAllCarTypesAsync()
        {
            return _mapper.Map<List<CarTypeDto>>(await _carTypeRepository.GetAllAsync());
        }
        public async Task<CarTypeDto> GetCarTypeByIdAsync(int id)
        {
            var carType = await _carTypeRepository.GetByIdAsync(id);
            if (carType == null)
            {
                throw new KeyNotFoundException("Car type not found");
            }
            return _mapper.Map<CarTypeDto>(carType);
        }
        public async Task AddCarTypeAsync(CarTypeDto carType)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(carType, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(carType, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            await _carTypeRepository.AddAsync(_mapper.Map<CarType>(carType));
        }
        public async Task UpdateCarTypeAsync(CarTypeDto carType)
        {
            var existingCarType = await _carTypeRepository.GetByIdAsync(carType.CarTypeId);
            if(existingCarType == null)
            {
                throw new KeyNotFoundException("Car type not found");
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(carType, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(carType, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            await _carTypeRepository.UpdateAsync(_mapper.Map(carType, existingCarType));
        }
        public async Task DeleteCarTypeAsync(int id)
        {
            var carType = await _carTypeRepository.GetByIdAsync(id);
            if (carType == null)
            {
                throw new KeyNotFoundException("Car type not found");
            }
            await _carTypeRepository.DeleteAsync(carType);
        }
    }
}
