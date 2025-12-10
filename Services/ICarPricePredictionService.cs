using Apetrei_Alexandru_Proiect.Models;
using System.Threading.Tasks;

namespace Apetrei_Alexandru_Proiect.Services
{
    public interface ICarPricePredictionService
    {
        Task<float> PredictPriceAsync(CarPriceInput input);
    }
}
