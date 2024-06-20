using AutoMapper;
using CarRentalServer.Repository.Entities;
using CarRentalServer.Repository.Repositories;
using CarRentalServer.Repository.Repositories.ModelRepository;
using CarRentalServer.Service.DTOs;
using CarRentalServer.Service.Services.BrandService;
using CarRentalServer.Service.Services.CarTypeService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.ModelService
{
    public class ModelService: IModelService
    {
        private readonly IModelRepository _modelRepository;
        private readonly IBrandService _brandService;
        private readonly ICarTypeService _carTypeService;
        private readonly IMapper _mapper;
        public ModelService(IModelRepository modelRepository, IBrandService brandService, ICarTypeService carTypeService, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _brandService = brandService;
            _carTypeService = carTypeService;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ModelDto>> GetAllModelsAsync()
        {
            return _mapper.Map<List<ModelDto>>(await _modelRepository.GetAllWithIncludesAsync());
        }

        public async Task<ModelDto> GetModelByIdAsync(int id)
        {
            var model = await _modelRepository.GetByIdWithIncludesAsync(id);
            if (model == null)
            {
                throw new KeyNotFoundException("Model not found");
            }

            return _mapper.Map<ModelDto>(model);
        }

        public async Task<ModelDto> AddModelAsync(ModelDto model)
        {
            try
            {
                await _brandService.GetBrandByIdAsync(model.BrandId);
                await _carTypeService.GetCarTypeByIdAsync(model.CarTypeId);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            var modelEntity = _mapper.Map<Model>(model);
            await _modelRepository.AddAsync(modelEntity);
            return await GetModelByIdAsync(modelEntity.ModelId);
        }

        public async Task UpdateModelAsync(ModelDto model)
        {
            var existingModel = await _modelRepository.GetByIdNoTrackingAsync(model.ModelId);
            if (existingModel == null)
            {
                throw new KeyNotFoundException("Model not found");
            }

            try
            {
                await _brandService.GetBrandByIdAsync(model.BrandId);
                await _carTypeService.GetCarTypeByIdAsync(model.CarTypeId);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            await _modelRepository.UpdateAsync(_mapper.Map<Model>(model));
        }

        public async Task DeleteModelAsync(int id)
        {
            var model = await _modelRepository.GetByIdAsync(id);
            if (model == null)
            {
                throw new KeyNotFoundException("Model not found");
            }
            await _modelRepository.DeleteAsync(model);
        }
    }
}
