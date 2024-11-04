using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage2.Models.Entities;
using Garage2.Models.ViewModels;
using Garage2.Migrations;
using System.Text.Json;

namespace Garage2.Data
{
    public class Garage2Context : DbContext
    {
        public Garage2Context (DbContextOptions<Garage2Context> options)
            : base(options)
        {
        }

        public DbSet<ParkedVehicle> ParkedVehicle { get; set; } = default!;
        public DbSet<OverviewViewModel> OverviewViewModel { get; set; } = default!;

        public DbSet<ParkingSpace> ParkingSpaces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //ToDo: Get capacity from config
            //Adding config to access garage capacity from there
            //IConfiguration config = new ConfigurationBuilder()
            //.SetBasePath(Environment.CurrentDirectory)
            //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //.Build();
            //var capacity = config.GetSection("garageOne:capacity").Value;

            //var configPath = Path.Combine(Directory.GetCurrentDirectory(), "GarageConfig.json");
            //var configData = File.ReadAllText(configPath);
            //var garageConfig = JsonSerializer.Deserialize<GarageConfig>(configData);

            //Seed the number of parking spots based on garage capacity from JSON

           //int capacity = garageConfig?.garageOne.capacity ?? 30; // Default to 30 if JSON data is missing

            for (int i = 1; i <= 30; i++)
            {
                modelBuilder.Entity<ParkingSpot>().HasData(new ParkingSpace
                {
                    ParkingId = i,
                    Points = 0 
                });
            }
        }

    }
}
