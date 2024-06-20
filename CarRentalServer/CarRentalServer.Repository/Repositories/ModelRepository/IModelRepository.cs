using CarRentalServer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Repository.Repositories.ModelRepository
{
    public interface IModelRepository : IGenericRepository<Model>
    {
        Task<IEnumerable<Model>> GetAllWithIncludesAsync();
        Task<Model> GetByIdWithIncludesAsync(int id);
        Task<Model> GetByIdNoTrackingAsync(int id);   
    }
}
