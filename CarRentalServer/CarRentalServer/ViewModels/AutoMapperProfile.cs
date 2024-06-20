using AutoMapper;
using CarRentalServer.Repository.Entities;
using CarRentalServer.Service.DTOs;

namespace CarRentalServer.API.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CarTypeDto, CarType>().ReverseMap();
            CreateMap<CarTypeDto, CarTypeViewModelGet>();
            CreateMap<CarTypeViewModelPost, CarTypeDto>();
            CreateMap<CarTypeViewModelPut, CarTypeDto>();

            CreateMap<CarDto, Car>().ReverseMap();
            CreateMap<CarDto, CarViewModelGet>();
            CreateMap<CarViewModelPost, CarDto>();
            CreateMap<CarViewModelPut, CarDto>();

            CreateMap<LocationDto, Location>().ReverseMap();
            CreateMap<LocationDto, LocationViewModelGet>();
            CreateMap<LocationViewModelPost, LocationDto>();
            CreateMap<LocationViewModelPut, LocationDto>();

            CreateMap<LocationCarDto, LocationCar>().ReverseMap();
            CreateMap<LocationCarDto, LocationCarViewModelGet>();
            CreateMap<LocationCarViewModelPost, LocationCarDto>();
            CreateMap<LocationCarViewModelPut, LocationCarDto>();
        }
    }
}
