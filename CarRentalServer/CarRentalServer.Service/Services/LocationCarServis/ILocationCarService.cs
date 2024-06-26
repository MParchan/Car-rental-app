﻿using CarRentalServer.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.LocationCarServis
{
    public interface ILocationCarService
    {
        Task<IEnumerable<LocationCarDto>> GetAllLocationCarsAsync();
        Task<IEnumerable<LocationCarDto>> GetLocationCarsByLocationIdAndModelId(int locationId, int modelId);
        Task<LocationCarDto> GetLocationCarByIdAsync(int id);
        Task<LocationCarDto> GetLocationCarByLocationIdAndCarIdAsync(int locationId, int carId);
        Task<LocationCarDto> AddLocationCarAsync(LocationCarDto locationCar);
        Task UpdateLocationCarAsync(LocationCarDto locationCar);
        Task DeleteLocationCarAsync(int id);
        Task RentCarAsync(int locationId, int carId);
        Task ReturnCarAsync(int locationId, int carId);    
    }
}
