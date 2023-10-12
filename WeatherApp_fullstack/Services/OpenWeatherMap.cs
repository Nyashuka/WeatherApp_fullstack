namespace WeatherApp_fullstack.Services
{
    public class OpenWeatherMap
    {
        private string? _apiKey;

        private const string CurrentWeatherQuery =
            "https://api.openweathermap.org/data/2.5/weather?appid=&units=metric&lang=en&q=";

        private string _request;

        public OpenWeatherMap(string? apiKey)
        {
            _request = CurrentWeatherQuery;
            SetApiKey(apiKey);
        }

        public async Task<WeatherData> GetCurrentWeatherByCity(string? cityName)
        {
            PasteDataInRequest(RequestParameters.City, cityName);

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(_request);
            string json = await response.Content.ReadAsStringAsync();
            
            WeatherData data = Serializator.Instance.Deserialize<WeatherData>(json);
            
            return data;
        }

        private void SetApiKey(string? apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new Exception("Api key is empty!");

            _apiKey = apiKey;

            PasteDataInRequest(RequestParameters.Token, apiKey);
        }

        private void PasteDataInRequest(string parameterKey, string? parameterValue)
        {
            int indexOfApi = _request.IndexOf(parameterKey, StringComparison.Ordinal);

            if (parameterValue != null)
                _request = _request.Insert(indexOfApi + parameterKey.Length, parameterValue);
        }
    }
}