using CarRentalServer.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.ModelService
{
    public interface IModelService
    {
        Task<IEnumerable<ModelDto>> GetAllModelsAsync();
        Task<ModelDto> GetModelByIdAsync(int id);
        Task<ModelDto> AddModelAsync(ModelDto model);
        Task UpdateModelAsync(ModelDto model);
        Task DeleteModelAsync(int id);
    }
}
