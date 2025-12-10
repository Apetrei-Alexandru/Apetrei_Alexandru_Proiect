using System.ComponentModel.DataAnnotations;

namespace Apetrei_Alexandru_Proiect.Models
{
    public class CarPricePredictionViewModel
    {
        // INPUT — ce completează utilizatorul
        [Required(ErrorMessage = "Brand is required")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(1900, 2100, ErrorMessage = "Enter a valid year")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Mileage is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Mileage must be positive")]
        public int Mileage { get; set; }

        // REZULTAT — ce întoarce API-ul
        public float? PredictedPrice { get; set; }
    }
}
