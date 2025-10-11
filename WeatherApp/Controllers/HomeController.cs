using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        List<CityWeather> cities = new List<CityWeather> {
        new CityWeather
        {
            CityUniqueCode = "LDN",
            CityName = "London",                
            DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),
            TemperatureFahrenheit = 33
        },
        new CityWeather
        {
            CityUniqueCode = "NYC",
            CityName = "London",
            DateAndTime = Convert.ToDateTime("2030-01-01 3:00"),
            TemperatureFahrenheit = 60
        },
        new CityWeather
        {
            CityUniqueCode = "PAR",
            CityName = "Paris",
            DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),
            TemperatureFahrenheit = 82
        }
    };


        [Route("/")]
        public IActionResult Index()
        {
            return View(cities);
        }

        [Route("/weather/{cityCode}")]
        public IActionResult City(string? cityCode)
        {
            if (string.IsNullOrEmpty(cityCode))
            {
                return View("Index");
            }

            CityWeather city = cities.Where(myCity => myCity.CityUniqueCode == cityCode ).FirstOrDefault();
            return View("city",city);
        }
    }
}
