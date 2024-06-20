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

namespace CarRentalServer.Service.Services.BrandService
{
    public class BrandService: IBrandService
    {
        private readonly IGenericRepository<Brand> _brandRepository;
        private readonly IMapper _mapper;
        public BrandService(IGenericRepository<Brand> brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            return _mapper.Map<List<BrandDto>>(await _brandRepository.GetAllAsync());
        }

        public async Task<BrandDto> GetBrandByIdAsync(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
            {
                throw new KeyNotFoundException("Brand not found");
            }
            return _mapper.Map<BrandDto>(brand);
        }

        public async Task<BrandDto> AddBrandAsync(BrandDto brand)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(brand, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(brand, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            var brandEntity = _mapper.Map<Brand>(brand);
            await _brandRepository.AddAsync(brandEntity);
            return await GetBrandByIdAsync(brandEntity.BrandId);
        }

        public async Task UpdateBrandAsync(BrandDto brand)
        {
            var existingBrand = await _brandRepository.GetByIdAsync(brand.BrandId);
            if (existingBrand == null)
            {
                throw new KeyNotFoundException("Brand not found");
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(brand, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(brand, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            await _brandRepository.UpdateAsync(_mapper.Map(brand, existingBrand));
        }

        public async Task DeleteBrandAsync(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
            {
                throw new KeyNotFoundException("Brand not found");
            }
            await _brandRepository.DeleteAsync(brand);
        }
    }
}
