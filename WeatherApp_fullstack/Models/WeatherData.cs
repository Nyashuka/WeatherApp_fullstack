using Newtonsoft.Json;
using WeatherApp_fullstack.Models;

namespace WeatherApp_fullstack;

public class WeatherData
{
    public List<Weather> Weathers => _weathers;

    public MainData MainData => _mainData;

    public Wind Wind => _wind;

    public int DateTimeUtc => _dateTimeUtc;
    public DateTime DateTime { get; private set; }

    [JsonProperty("weather")] private List<Weather> _weathers;
    [JsonProperty("main")] private MainData _mainData;
    [JsonProperty("wind")] private Wind _wind;
    [JsonProperty("dt")] private int _dateTimeUtc;
    [JsonProperty("name")] private string _cityName;
    [JsonProperty("id")] private int _cityId;

    public string CityName => _cityName;

    public int CityId => _cityId;

    public WeatherData([JsonProperty("weather")] List<Weather> weathers, [JsonProperty("main")] MainData mainData, 
        [JsonProperty("wind")] Wind wind, [JsonProperty("dt")] int dateTimeUtc, [JsonProperty("name")] string cityName,
        [JsonProperty("id")] int cityId)
    {
        _weathers = weathers;
        _mainData = mainData;
        _wind = wind;
        _dateTimeUtc = dateTimeUtc;
        _cityName = cityName;
        _cityId = cityId;
        DateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(DateTimeUtc);
    }

    public override string ToString()
    {
        return $"City id: {_cityId},\nCity name: {_cityName},\nTemperature: {_mainData.Temp},\nTime: {DateTime},\nStatus: {_weathers[0].Status},\nDescription: {_weathers[0].Description}.\n";
    }
}