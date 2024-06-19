using AutoMapper;
using CarRentalServer.Repository.Entities;
using CarRentalServer.Repository.Repositories.CarTypeRepository;
using CarRentalServer.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.CarTypeService
{
    public class CarTypeService : ICarTypeService
    {
        private readonly ICarTypeRepository _carTypeRepository;
        private readonly IMapper _mapper;
        public CarTypeService(ICarTypeRepository carTypeRepository, IMapper mapper)
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
            return _mapper.Map<CarTypeDto>(await _carTypeRepository.GetByIdAsync(id));
        }
        public async Task AddCarTypeAsync(CarTypeDto carType)
        {
            await _carTypeRepository.AddAsync(_mapper.Map<CarType>(carType));
        }
        public async Task UpdateCarTypeAsync(CarTypeDto carType)
        {
            await _carTypeRepository.UpdateAsync(_mapper.Map<CarType>(carType));
        }
        public async Task DeleteCarTypeAsync(int id)
        {
            var carType = await _carTypeRepository.GetByIdAsync(id);
            await _carTypeRepository.DeleteAsync(carType);
        }
    }
}
