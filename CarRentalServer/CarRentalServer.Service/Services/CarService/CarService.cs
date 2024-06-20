using AutoMapper;
using CarRentalServer.Repository.Entities;
using CarRentalServer.Repository.Repositories.CarRepository;
using CarRentalServer.Service.DTOs;
using CarRentalServer.Service.Services.CarTypeService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.CarService
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarTypeService _carTypeService;
        private readonly IMapper _mapper;
        public CarService(ICarRepository carRepository, ICarTypeService carTypeService, IMapper mapper)
        {
            _carRepository = carRepository;
            _carTypeService = carTypeService;
            _mapper = mapper;
        }


        public async Task<IEnumerable<CarDto>> GetAllCarsAsync()
        {
            return _mapper.Map<List<CarDto>>(await _carRepository.GetAllWithIncludesAsync());
        }

        public async Task<CarDto> GetCarByIdAsync(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null)
            {
                throw new KeyNotFoundException("Car not found");
            }

            return _mapper.Map<CarDto>(car);
        }

        public async Task<CarDto> AddCarAsync(CarDto car)
        {
            try
            {
                await _carTypeService.GetCarTypeByIdAsync(car.CarTypeId);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(car, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(car, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }   
            car.PricePerDay = Math.Round(car.PricePerDay, 2);

            var carEntity = _mapper.Map<Car>(car);
            await _carRepository.AddAsync(carEntity);
            return await GetCarByIdAsync(carEntity.CarId);
        }

        public async Task UpdateCarAsync(CarDto car)
        {
            var existingCar = await _carRepository.GetByIdNoTrackingAsync(car.CarId);
            if (existingCar == null)
            {
                throw new KeyNotFoundException("Car not found");
            }

            try
            {
                await _carTypeService.GetCarTypeByIdAsync(car.CarTypeId);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(car, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(car, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }
            car.PricePerDay = Math.Round(car.PricePerDay, 2);

            await _carRepository.UpdateAsync(_mapper.Map<Car>(car));
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null)
            {
                throw new KeyNotFoundException("Car not found");
            }
            await _carRepository.DeleteAsync(car);
        }
    }
}
