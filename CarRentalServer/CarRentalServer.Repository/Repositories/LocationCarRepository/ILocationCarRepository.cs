﻿using CarRentalServer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Repository.Repositories.LocationCarRepository
{
    public interface ILocationCarRepository : IGenericRepository<LocationCar>
    {
        Task<IEnumerable<LocationCar>> GetAllWithIncludesAsync();
        Task<LocationCar> GetByIdWithIncludesAsync(int id);
        Task<LocationCar> GetByIdNoTrackingAsync(int id);
    }
}