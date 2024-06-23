using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Repository.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationCar> LocationCars { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LocationCar>(entity =>
            {
                entity.HasKey(lc => lc.LocationCarId);

                entity.HasOne(lc => lc.Location)
                    .WithMany()
                    .HasForeignKey(lc => lc.LocationId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(lc => lc.Car)
                    .WithMany()
                    .HasForeignKey(lc => lc.CarId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(r => r.ReservationId);

                entity.HasOne(r => r.Car)
                    .WithMany()
                    .HasForeignKey(r => r.CarId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.RentalLocation)
                    .WithMany()
                    .HasForeignKey(r => r.RentalLocationId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.ReturnLocation)
                    .WithMany()
                    .HasForeignKey(r => r.ReturnLocationId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(c => c.CarId);

                entity.HasOne(c => c.Model)
                    .WithMany()
                    .HasForeignKey(c => c.ModelId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.HasKey(m => m.ModelId);

                entity.HasOne(m => m.Brand)
                    .WithMany()
                    .HasForeignKey(m => m.BrandId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(m => m.CarType)
                    .WithMany()
                    .HasForeignKey(m => m.CarTypeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
