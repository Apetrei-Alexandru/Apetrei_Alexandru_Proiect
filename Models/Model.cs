using System.Collections.Generic;
namespace Apetrei_Alexandru_Proiect.Models
{
    public class Model
    {
        public int ModelId { get; set; }
        public string Name { get; set; }

        // Foreign Key
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }

        // Navigation
        public ICollection<Car>? Cars { get; set; }
    }
}
