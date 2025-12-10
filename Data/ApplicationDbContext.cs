using Apetrei_Alexandru_Proiect.Models;
using Microsoft.EntityFrameworkCore;

namespace Apetrei_Alexandru_Proiect.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<Condition> Conditions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relația Model → Brand (cascade OK)
            modelBuilder.Entity<Model>()
                .HasOne(m => m.Brand)
                .WithMany(b => b.Models)
                .HasForeignKey(m => m.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relațiile Car → Brand/Model/FuelType/Transmission/Condition
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Brand)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Model)
                .WithMany(m => m.Cars)
                .HasForeignKey(c => c.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.FuelType)
                .WithMany(f => f.Cars)
                .HasForeignKey(c => c.FuelTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Transmission)
                .WithMany(t => t.Cars)
                .HasForeignKey(c => c.TransmissionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Condition)
                .WithMany(cd => cd.Cars)
                .HasForeignKey(c => c.ConditionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
