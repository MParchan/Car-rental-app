using AutoMapper;
using CarRentalServer.Repository.Entities;
using CarRentalServer.Repository.Repositories;
using CarRentalServer.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.CarService
{
    public class CarService : ICarService
    {
        private readonly IGenericRepository<Car> _carRepository;
        private readonly IMapper _mapper;
        public CarService(IGenericRepository<Car> carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarDto>> GetAllCarsAsync()
        {
            return _mapper.Map<List<CarDto>>(await _carRepository.GetAllAsync());
        }
        public async Task<CarDto> GetCarByIdAsync(int id)
        {
            return _mapper.Map<CarDto>(await _carRepository.GetByIdAsync(id));
        }
        public async Task AddCarAsync(CarDto car)
        {
            await _carRepository.AddAsync(_mapper.Map<Car>(car));
        }
        public async Task UpdateCarAsync(CarDto car)
        {
            await _carRepository.UpdateAsync(_mapper.Map<Car>(car));
        }
        public async Task DeleteCarAsync(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            await _carRepository.DeleteAsync(car);
        }
    }
}
