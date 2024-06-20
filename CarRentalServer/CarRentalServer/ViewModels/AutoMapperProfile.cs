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
            CreateMap<CarTypeDto, CarTypeViewModelGet>().ReverseMap();
            CreateMap<CarTypeViewModelPost, CarTypeDto>().ReverseMap();
            CreateMap<CarTypeViewModelPut, CarTypeDto>().ReverseMap();

            CreateMap<CarDto, Car>().ReverseMap();
            CreateMap<CarDto, CarViewModelGet>();
            CreateMap<CarViewModelPost, CarDto>();
            CreateMap<CarViewModelPut, CarDto>();
        }
    }
}
