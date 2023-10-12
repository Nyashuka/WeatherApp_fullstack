using Newtonsoft.Json;

namespace WeatherApp_fullstack.Models;

public class Wind
{
    [JsonProperty("speed")]
    private float _speed;

    public float Speed => _speed;

    public Wind([JsonProperty("speed")] float speed)
    {
        _speed = speed;
    }
}