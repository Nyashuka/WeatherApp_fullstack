using WeatherApp_fullstack.Services;

namespace WeatherApp_fullstack
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string? apiKey = Environment.GetEnvironmentVariable("weather_api_key");

            if (string.IsNullOrEmpty(apiKey))
                return;
            
            OpenWeatherMap openWeatherMap = new OpenWeatherMap(apiKey);
            WeatherData weatherData = await openWeatherMap.GetCurrentWeatherByCity("Zvenyhorodka");
            Console.WriteLine(weatherData);
            
            DataBaseController dataBaseController = new DataBaseController();
            
            dataBaseController.AddToDataBase(weatherData);
            dataBaseController.ShowAllRecords();
        }

        
    }
}