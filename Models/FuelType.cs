using System.Collections.Generic;
namespace Apetrei_Alexandru_Proiect.Models
{
    public class FuelType
    {
        public int FuelTypeId { get; set; }
        public string Name { get; set; }

        // Navigation
        public ICollection<Car>? Cars { get; set; }
    }
}
