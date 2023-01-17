using System.Net.Http.Headers;

namespace ZeClient;
class Program
{
    static void Main(string[] args)
    {
        Simple();
        System.Console.WriteLine(   "Etc...");
        Console.ReadLine();
    }

    private static async Task Simple()
    {
         HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7078/");
        for(int i = 0; i < 500; i++)
        {
           
            var response = await client.GetAsync("WeatherForecast");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                System.Console.WriteLine(response.Content.Headers.ContentType);
                    //if (response.Content.Headers.ContentType == new MediaTypeHeaderValue("application/json"))
                {
                    var json = await response.Content.ReadAsStringAsync();
                    //System.Console.WriteLine(json);
                }
            }
            
        }
    }
}
