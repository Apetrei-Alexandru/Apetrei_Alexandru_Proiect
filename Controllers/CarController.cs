using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using PricePredictorGrpcService;

namespace Apetrei_Alexandru_Proiect.Controllers
{
    public class CarController : Controller
    {
        private readonly GrpcChannel _channel;

        public CarController()
        {
            // ATENȚIE: modifică portul către serverul tău gRPC!
            _channel = GrpcChannel.ForAddress("https://localhost:7208");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Predict()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Predict(string brand, string model, int year, float mileage)
        {
            var client = new PricePredictor.PricePredictorClient(_channel);

            var request = new PricePredictionRequest
            {
                Brand = brand,
                Model = model,
                Year = year,
                Mileage = mileage
            };

            // apelăm gRPC-ul
            var reply = await client.PredictAsync(request);

            float price = reply.PredictedPrice;

            // Trimitem date către View
            ViewBag.Brand = brand;
            ViewBag.Model = model;
            ViewBag.Year = year;
            ViewBag.Mileage = mileage;
            ViewBag.Price = price;

            // Mesaj în funcție de intervalul prețului
            string message;

            if (price < 20000)
                message = "Preț mic (sub 20.000 €)";
            else if (price >= 20000 && price <= 50000)
                message = "Preț mediu (între 20.000 € și 50.000 €)";
            else
                message = "Preț mare (peste 50.000 €)";

            ViewBag.Message = message;

            return View();
        }
    }
}
