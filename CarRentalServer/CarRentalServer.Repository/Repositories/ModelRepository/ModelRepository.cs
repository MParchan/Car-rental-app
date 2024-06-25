using CarRentalServer.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Repository.Repositories.ModelRepository
{
    public class ModelRepository : GenericRepository<Model>, IModelRepository
    {
        private readonly AppDbContext _context;
        public ModelRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Model>> GetAllWithIncludesAsync()
        {
            return await _context.Models
                .Include(model => model.Brand)
                .Include(model => model.CarType)
                .ToListAsync();
        }
        public async Task<Model> GetByIdWithIncludesAsync(int id)
        {
            return await _context.Models
                .Include(model => model.Brand)
                .Include(model => model.CarType)
                .Include(model => model.Cars)
                .Select(m => new Model
                {
                    ModelId = m.ModelId,
                    Name = m.Name,
                    SeatsNumber = m.SeatsNumber,
                    ImageUrl = m.ImageUrl,
                    Brand = m.Brand,
                    CarType = m.CarType,
                    Cars = m.Cars.Select(c => new Car
                    {
                        CarId = c.CarId,
                        Version = c.Version,
                        PricePerDay = c.PricePerDay,
                        ProductionYear = c.ProductionYear,
                        Horsepower = c.Horsepower,
                        Range = c.Range,
                        ModelId = c.ModelId
                    }).ToList()
                })
                .FirstOrDefaultAsync(model => model.ModelId == id);
        }
        public async Task<Model> GetByIdNoTrackingAsync(int id)
        {
            return await _context.Models
                .AsNoTracking()
                .FirstOrDefaultAsync(model => model.ModelId == id);
        }
    }
}
