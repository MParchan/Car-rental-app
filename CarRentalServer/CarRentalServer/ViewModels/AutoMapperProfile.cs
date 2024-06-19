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
            CreateMap<CarTypeViewModel, CarTypeDto>().ReverseMap();

            CreateMap<CarDto, Car>().ReverseMap();
            CreateMap<CarViewModel, CarDto>().ReverseMap();
        }
    }
}
