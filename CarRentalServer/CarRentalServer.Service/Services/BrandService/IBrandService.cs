using CarRentalServer.Repository.Entities;
using CarRentalServer.Repository.Repositories;
using CarRentalServer.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.BrandService
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        Task<BrandDto> GetBrandByIdAsync(int id);
        Task<BrandDto> AddBrandAsync(BrandDto brand);
        Task UpdateBrandAsync(BrandDto brand);
        Task DeleteBrandAsync(int id);
    }
}
