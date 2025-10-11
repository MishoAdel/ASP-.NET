using Microsoft.Extensions.Configuration;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["api-token"]}")
            };

            HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);

            string responseBody = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();

            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            if (responseDictionary == null)
                throw new InvalidOperationException("No response from server");

            if (responseDictionary.ContainsKey("error"))
                throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

            return responseDictionary;

        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($" https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["api-token"]}")
            };

            HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);

            string responseBody = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();

            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            if (responseDictionary == null)
                throw new InvalidOperationException("No response from server");

            if (responseDictionary.ContainsKey("error"))
                throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

            return responseDictionary;
        }

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            HttpClient client = _httpClientFactory.CreateClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($" https://finnhub.io/api/v1//stock/symbol?exchange=US&token={_configuration["api-token"]}")
            };

            HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);

            string responseBody = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();

            List<Dictionary<string, string>>? responseDictionary = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(responseBody);

            if (responseDictionary == null)
                throw new InvalidOperationException("No response from server");

            return responseDictionary;
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($" https://finnhub.io/api/v1/search?q={stockSymbolToSearch}&token={_configuration["api-token"]}")
            };

            HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);

            string responseBody = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();

            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            if (responseDictionary == null)
                throw new InvalidOperationException("No response from server");

            if (responseDictionary.ContainsKey("error"))
                throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

            return responseDictionary;
        }
    }
}
