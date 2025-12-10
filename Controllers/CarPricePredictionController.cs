using Microsoft.AspNetCore.Mvc;
using Apetrei_Alexandru_Proiect.Models;
using Apetrei_Alexandru_Proiect.Services;
using System.Threading.Tasks;

namespace Apetrei_Alexandru_Proiect.Controllers
{
    public class CarPricePredictionController : Controller
    {
        private readonly ICarPricePredictionService _carPriceService;

        public CarPricePredictionController(ICarPricePredictionService carPriceService)
        {
            _carPriceService = carPriceService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new CarPricePredictionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(CarPricePredictionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // returnează view-ul cu valorile introduse și mesajele de validare
                return View(model);
            }

            var input = new CarPriceInput
            {
                Brand = model.Brand,
                Model = model.Model,
                Year = model.Year,
                Mileage = model.Mileage
            };

            // apel API pentru predicție
            var prediction = await _carPriceService.PredictPriceAsync(input);

            model.PredictedPrice = prediction;

            // returnăm view-ul cu valorile completate și predicția
            return View(model);
        }
    }
}
