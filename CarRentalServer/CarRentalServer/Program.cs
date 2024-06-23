using CarRentalServer.Repository.Entities;
using CarRentalServer.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using CarRentalServer.Service.Services.CarTypeService;
using CarRentalServer.Service.Services.CarService;
using CarRentalServer.API.ViewModels;
using CarRentalServer.Repository.Repositories.CarRepository;
using CarRentalServer.Service.Services.LocationServis;
using CarRentalServer.Repository.Repositories.LocationCarRepository;
using CarRentalServer.Service.Services.LocationCarServis;
using CarRentalServer.Service.Services.BrandService;
using CarRentalServer.Repository.Repositories.ModelRepository;
using CarRentalServer.Service.Services.ModelService;
using CarRentalServer.Repository.Repositories.ReservationRepository;
using CarRentalServer.Service.Services.ReservationService;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));

// Add services to the container.
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICarTypeService, CarTypeService>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ILocationServis, LocationServis>();
builder.Services.AddScoped<ILocationCarRepository, LocationCarRepository>();
builder.Services.AddScoped<ILocationCarServis, LocationCarService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IModelRepository, ModelRepository>();
builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
