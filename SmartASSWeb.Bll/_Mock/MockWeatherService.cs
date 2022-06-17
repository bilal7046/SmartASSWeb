using System.Threading.Tasks;
using SmartASSWeb.Bll.Services;

namespace SmartASSWeb.Bll._Mock
{
    public class MockWeatherService
        : IWeatherService
    {
        public Task<WeatherResult> GetWeatherDetail(string city)
        {
            var result = new WeatherResult
            {
                City = "Johannesburg"
            };
            return Task.FromResult(result);
        }
    }
}
