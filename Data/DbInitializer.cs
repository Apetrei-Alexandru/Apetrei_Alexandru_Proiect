using Apetrei_Alexandru_Proiect.Models;
using Microsoft.EntityFrameworkCore;

namespace Apetrei_Alexandru_Proiect.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Cars.Any()) return;

                // Brand
                var brands = new Brand[]
                {
                    new Brand { Name="Toyota" },
                    new Brand { Name="BMW" },
                    new Brand { Name="Ford" }
                };
                context.Brands.AddRange(brands);
                context.SaveChanges();

                // Model
                var models = new Model[]
                {
                    new Model { Name="Corolla", BrandId=brands[0].BrandId },
                    new Model { Name="Camry", BrandId=brands[0].BrandId },
                    new Model { Name="X5", BrandId=brands[1].BrandId }
                };
                context.Models.AddRange(models);
                context.SaveChanges();

                // FuelType
                var fuels = new FuelType[]
                {
                    new FuelType { Name="Petrol" },
                    new FuelType { Name="Diesel" },
                    new FuelType { Name="Electric" }
                };
                context.FuelTypes.AddRange(fuels);
                context.SaveChanges();

                // Transmission
                var transmissions = new Transmission[]
                {
                    new Transmission { Name="Manual" },
                    new Transmission { Name="Automatic" }
                };
                context.Transmissions.AddRange(transmissions);
                context.SaveChanges();

                // Condition
                var conditions = new Condition[]
                {
                    new Condition { Name="New" },
                    new Condition { Name="Used" },
                    new Condition { Name="Like New" }
                };
                context.Conditions.AddRange(conditions);
                context.SaveChanges();

                // Car
                var cars = new Car[]
                {
                    new Car {
                        Year=2020,
                        EngineSize=2.0m,
                        Mileage=15000,
                        Price=20000,
                        BrandId=brands[0].BrandId,
                        ModelId=models[0].ModelId,
                        FuelTypeId=fuels[0].FuelTypeId,
                        TransmissionId=transmissions[0].TransmissionId,
                        ConditionId=conditions[0].ConditionId
                    },
                    new Car {
                        Year=2019,
                        EngineSize=3.0m,
                        Mileage=30000,
                        Price=25000,
                        BrandId=brands[1].BrandId,
                        ModelId=models[2].ModelId,
                        FuelTypeId=fuels[1].FuelTypeId,
                        TransmissionId=transmissions[1].TransmissionId,
                        ConditionId=conditions[1].ConditionId
                    }
                };
                context.Cars.AddRange(cars);

                context.SaveChanges();
            }
        }
    }
}
