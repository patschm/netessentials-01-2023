using Newtonsoft.Json;

namespace HttpClientApp;

public class WeatherForecastProxy
{
    private readonly HttpClient _httpClient;

    public WeatherForecastProxy(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public IEnumerable<WeatherForecast>? GetWeather()
    {
        var response = _httpClient.GetAsync("WeatherForecast").Result;
        if (response.IsSuccessStatusCode)
        {
            var strData = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<WeatherForecast>>(strData);
        }
        return null;
    }
}
