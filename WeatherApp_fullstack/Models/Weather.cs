using Newtonsoft.Json;

namespace WeatherApp_fullstack.Models;

public class Weather
{
    [JsonProperty("main")]
    private string? _status;

    public string? Status => _status;

    public string? Description => _description;

    [JsonProperty("description")]
    private string? _description;

    [JsonConstructor]
    public Weather([JsonProperty("main")]string? status, 
                    [JsonProperty("description")]string? description)
    {
        _status = status;
        _description = description;
    }
}