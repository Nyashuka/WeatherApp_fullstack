using Newtonsoft.Json;

namespace WeatherApp_fullstack.Models;

public class MainData
{
    [JsonProperty("temp")] private float _temp;

    public float Temp => _temp;

    public MainData([JsonProperty("temp")] float temp)
    {
        _temp = temp;
    }
}