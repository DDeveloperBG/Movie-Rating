using System.Text.Json;

namespace MovieRatingApp.Models.Http
{
    public class HttpHelperService
    {
        private readonly HttpClient httpClient;

        public HttpHelperService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<T> MakeApiGetRequestAsync<T>(string url, string apiHeader, string apiKey)
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            requestMessage.Headers.Add(apiHeader, apiKey);

            using HttpResponseMessage response = await httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<T>(responseBody, options);
        }
    }
}
