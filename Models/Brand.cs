using System.Reflection;
using System.Collections.Generic;

namespace Apetrei_Alexandru_Proiect.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string Name { get; set; }

        // Navigation
        public ICollection<Model>? Models { get; set; }
        public ICollection<Car>? Cars { get; set; }
    }
}
