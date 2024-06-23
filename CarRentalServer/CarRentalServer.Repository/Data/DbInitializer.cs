using CarRentalServer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Repository.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();


            var roles = new Role[]
            {
                new Role{ Name = "Admin" },
                new Role{ Name = "User" }
            };
            foreach (Role role in roles)
            {
                context.Roles.Add(role);
            }
            context.SaveChanges();


            Brand brand = new() { Name = "Tesla" };
            context.Brands.Add(brand);
            context.SaveChanges();


            var carTypes = new CarType[]
            {
                new CarType { Name = "Sedan" },
                new CarType { Name = "Liftback" },
                new CarType { Name = "Crossover" },
                new CarType { Name = "Pickup" },
                new CarType { Name = "Coupe" }
            };
            foreach(CarType carType in carTypes)
            {
                context.CarTypes.Add(carType);
            }
            context.SaveChanges();


            var models = new Model[]
            {
                new Model
                {
                    BrandId = 1,
                    CarTypeId = 1,
                    Name = "Model 3",
                    SeatsNumber = 5,
                    ImageUrl = "https://res.cloudinary.com/dl9iq9r8m/image/upload/v1719160739/TeslaModel3_kafjej.png"
                },
                new Model
                {
                    BrandId = 1,
                    CarTypeId = 2,
                    Name = "Model S",
                    SeatsNumber = 5,
                    ImageUrl = "https://res.cloudinary.com/dl9iq9r8m/image/upload/v1719160741/TeslaModelS_qtgkoa.png"
                },
                new Model
                {
                    BrandId = 1,
                    CarTypeId = 3,
                    Name = "Model Y",
                    SeatsNumber = 7,
                    ImageUrl = "https://res.cloudinary.com/dl9iq9r8m/image/upload/v1719160739/TeslaModelY_kkmgu1.jpg"
                },
                new Model
                {
                    BrandId = 1,
                    CarTypeId = 3,
                    Name = "Model X",
                    SeatsNumber = 7,
                    ImageUrl = "https://res.cloudinary.com/dl9iq9r8m/image/upload/v1719160741/TeslaModelX_eyanpx.png"
                },
                new Model
                {
                    BrandId = 1,
                    CarTypeId = 4,
                    Name = "Cybertruck",
                    SeatsNumber = 5,
                    ImageUrl = "https://res.cloudinary.com/dl9iq9r8m/image/upload/v1719160739/TeslaCybertruck_ttfbfz.jpg"
                },
                new Model
                {
                    BrandId = 1,
                    CarTypeId = 5,
                    Name = "Roadster",
                    SeatsNumber = 4,
                    ImageUrl = "https://res.cloudinary.com/dl9iq9r8m/image/upload/v1719160739/TeslaRoadster_z2wkje.jpg"
                },
            };
            foreach (Model model in models)
            {
                context.Models.Add(model);
            }
            context.SaveChanges();


            var cars = new Car[]
            {
                //Model 3
                new Car
                {
                    ModelId = 1,
                    Version = "Maximum Range 80.5kWh",
                    PricePerDay = 32.99m,
                    ProductionYear = 2020,
                    Horsepower = 441,
                    Range = 602
                },
                new Car
                {
                    ModelId = 1,
                    Version = "Maximum Range 80.5kWh",
                    PricePerDay = 36.99m,
                    ProductionYear = 2022,
                    Horsepower = 441,
                    Range = 602
                },
                new Car
                {
                    ModelId = 1,
                    Version = "Performance 80.5kWh",
                    PricePerDay = 39.99m,
                    ProductionYear = 2022,
                    Horsepower = 513,
                    Range = 567
                },
                //Model S
                new Car
                {
                    ModelId = 2,
                    Version = "Long Range 100kWh",
                    PricePerDay = 89.99m,
                    ProductionYear = 2022,
                    Horsepower = 1020,
                    Range = 637
                },
                new Car
                {
                    ModelId = 2,
                    Version = "Plaid 100kWh",
                    PricePerDay = 94.39m,
                    ProductionYear = 2022,
                    Horsepower = 1020,
                    Range = 637
                },
                new Car
                {
                    ModelId = 2,
                    Version = "Plaid 100kWh",
                    PricePerDay = 98.59m,
                    ProductionYear = 2024,
                    Horsepower = 1020,
                    Range = 637
                },
                //Model Y
                new Car
                {
                    ModelId = 3,
                    Version = "Standard Range 50kWh",
                    PricePerDay = 28.59m,
                    ProductionYear = 2021,
                    Horsepower = 204,
                    Range = 455
                },
                new Car
                {
                    ModelId = 3,
                    Version = "Long Range 75kWh",
                    PricePerDay = 31.99m,
                    ProductionYear = 2021,
                    Horsepower = 351,
                    Range = 507
                },
                new Car
                {
                    ModelId = 3,
                    Version = "Performance 75kWh",
                    PricePerDay = 38.99m,
                    ProductionYear = 2021,
                    Horsepower = 462,
                    Range = 488
                },
                new Car
                {
                    ModelId = 3,
                    Version = "Performance 80.5kWh",
                    PricePerDay = 45.99m,
                    ProductionYear = 2023,
                    Horsepower = 534,
                    Range = 514
                },
                //Model X
                new Car
                {
                    ModelId = 4,
                    Version = "Long Range 100kWh",
                    PricePerDay = 62.59m,
                    ProductionYear = 2022,
                    Horsepower = 670,
                    Range = 579
                },
                new Car
                {
                    ModelId = 4,
                    Version = "Plaid 100kWh",
                    PricePerDay = 91.99m,
                    ProductionYear = 2022,
                    Horsepower = 1020,
                    Range = 547
                },
                //Cybertruck
                new Car
                {
                    ModelId = 5,
                    Version = "Dual Motor AWD",
                    PricePerDay = 78.99m,
                    ProductionYear = 2023,
                    Horsepower = 600,
                    Range = 550
                },
                new Car
                {
                    ModelId = 5,
                    Version = "Tri Motor AWD",
                    PricePerDay = 96.99m,
                    ProductionYear = 2023,
                    Horsepower = 845,
                    Range = 510
                },
                //Roadster
                new Car
                {
                    ModelId = 5,
                    Version = "Tri Motor AWD",
                    PricePerDay = 129.99m,
                    ProductionYear = 2023,
                    Horsepower = 1360,
                    Range = 1000
                },
            };
            foreach (Car car in cars)
            {
                context.Cars.Add(car);
            }
            context.SaveChanges();


            var locations = new Location[]
            {
                new Location{ Name = "Palma Airport" },
                new Location{ Name = "Palma City Center" },
                new Location{ Name = "Alcudia" },
                new Location{ Name = "Manacor" }
            };
            foreach (Location location in locations)
            {
                context.Locations.Add(location);
            }
            context.SaveChanges();


            var locationCars = new LocationCar[]
            {
                //Palma Airport
                new LocationCar{ LocationId = 1, CarId = 1, Quantity=8 },
                new LocationCar{ LocationId = 1, CarId = 2, Quantity=4 },
                new LocationCar{ LocationId = 1, CarId = 3, Quantity=5 },
                new LocationCar{ LocationId = 1, CarId = 4, Quantity=8 },
                new LocationCar{ LocationId = 1, CarId = 5, Quantity=10 },
                new LocationCar{ LocationId = 1, CarId = 6, Quantity=3 },
                new LocationCar{ LocationId = 1, CarId = 7, Quantity=12 },
                new LocationCar{ LocationId = 1, CarId = 8, Quantity=4 },
                new LocationCar{ LocationId = 1, CarId = 9, Quantity=1 },
                new LocationCar{ LocationId = 1, CarId = 10, Quantity=0 },
                new LocationCar{ LocationId = 1, CarId = 11, Quantity=3 },
                new LocationCar{ LocationId = 1, CarId = 12, Quantity=4 },
                new LocationCar{ LocationId = 1, CarId = 13, Quantity=2 },
                new LocationCar{ LocationId = 1, CarId = 14, Quantity=2 },
                new LocationCar{ LocationId = 1, CarId = 15, Quantity=1 },
                //Palma City Center
                new LocationCar{ LocationId = 2, CarId = 1, Quantity=3 },
                new LocationCar{ LocationId = 2, CarId = 2, Quantity=4 },
                new LocationCar{ LocationId = 2, CarId = 3, Quantity=2 },
                new LocationCar{ LocationId = 2, CarId = 4, Quantity=8 },
                new LocationCar{ LocationId = 2, CarId = 5, Quantity=6 },
                new LocationCar{ LocationId = 2, CarId = 6, Quantity=3 },
                new LocationCar{ LocationId = 2, CarId = 7, Quantity=12 },
                new LocationCar{ LocationId = 2, CarId = 8, Quantity=5 },
                new LocationCar{ LocationId = 2, CarId = 9, Quantity=1 },
                new LocationCar{ LocationId = 2, CarId = 10, Quantity=8 },
                new LocationCar{ LocationId = 2, CarId = 11, Quantity=1 },
                new LocationCar{ LocationId = 2, CarId = 12, Quantity=4 },
                new LocationCar{ LocationId = 2, CarId = 13, Quantity=4 },
                new LocationCar{ LocationId = 2, CarId = 14, Quantity=3 },
                new LocationCar{ LocationId = 2, CarId = 15, Quantity=2 },
                //Alcudia
                new LocationCar{ LocationId = 3, CarId = 1, Quantity=0 },
                new LocationCar{ LocationId = 3, CarId = 2, Quantity=0 },
                new LocationCar{ LocationId = 3, CarId = 3, Quantity=2 },
                new LocationCar{ LocationId = 3, CarId = 4, Quantity=8 },
                new LocationCar{ LocationId = 3, CarId = 5, Quantity=0 },
                new LocationCar{ LocationId = 3, CarId = 6, Quantity=3 },
                new LocationCar{ LocationId = 3, CarId = 7, Quantity=1 },
                new LocationCar{ LocationId = 3, CarId = 8, Quantity=5 },
                new LocationCar{ LocationId = 3, CarId = 9, Quantity=1 },
                new LocationCar{ LocationId = 3, CarId = 10, Quantity=3 },
                new LocationCar{ LocationId = 3, CarId = 11, Quantity=1 },
                new LocationCar{ LocationId = 3, CarId = 12, Quantity=2 },
                new LocationCar{ LocationId = 3, CarId = 13, Quantity=1 },
                new LocationCar{ LocationId = 3, CarId = 14, Quantity=0 },
                new LocationCar{ LocationId = 3, CarId = 15, Quantity=0 },
                //Manacor
                new LocationCar{ LocationId = 4, CarId = 1, Quantity=12 },
                new LocationCar{ LocationId = 4, CarId = 2, Quantity=14 },
                new LocationCar{ LocationId = 4, CarId = 3, Quantity=2 },
                new LocationCar{ LocationId = 4, CarId = 4, Quantity=8 },
                new LocationCar{ LocationId = 4, CarId = 5, Quantity=23 },
                new LocationCar{ LocationId = 4, CarId = 6, Quantity=3 },
                new LocationCar{ LocationId = 4, CarId = 7, Quantity=7 },
                new LocationCar{ LocationId = 4, CarId = 8, Quantity=5 },
                new LocationCar{ LocationId = 4, CarId = 9, Quantity=1 },
                new LocationCar{ LocationId = 4, CarId = 10, Quantity=3 },
                new LocationCar{ LocationId = 4, CarId = 11, Quantity=8 },
                new LocationCar{ LocationId = 4, CarId = 12, Quantity=2 },
                new LocationCar{ LocationId = 4, CarId = 13, Quantity=10 },
                new LocationCar{ LocationId = 4, CarId = 14, Quantity=2 },
                new LocationCar{ LocationId = 4, CarId = 15, Quantity=5 }
            };
            foreach (LocationCar locationCar in locationCars)
            {
                context.LocationCars.Add(locationCar);
            }
            context.SaveChanges();
        }
    }
}
