using Microsoft.VisualBasic.FileIO;
using System.Reflection;

namespace Apetrei_Alexandru_Proiect.Models
{
    public class Car
    {
        public int CarId { get; set; }

        public int Year { get; set; }
        public decimal EngineSize { get; set; }
        public int Mileage { get; set; }
        public decimal Price { get; set; }

        // Foreign Keys
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }

        public int ModelId { get; set; }
        public Model? Model { get; set; }

        public int FuelTypeId { get; set; }
        public FuelType? FuelType { get; set; }

        public int TransmissionId { get; set; }
        public Transmission? Transmission { get; set; }

        public int ConditionId { get; set; }
        public Condition? Condition { get; set; }
    }
}
