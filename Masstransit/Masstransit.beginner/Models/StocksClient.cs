

using Newtonsoft.Json;

namespace Masstransit.beginner.Models;

public class StocksClient(HttpClient httpClient, IConfiguration configuration)
{
    public async Task<string> GetStockPrice(string symbol)
    {
        string apiKey = configuration["AlphaVantage:ApiKey"]!;
        string url = $"?function=TIME_SERIES_INTRADAY&symbol={symbol}&interval=1min&apikey={apiKey}";
        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();


        return response.ToString();
    }
}
