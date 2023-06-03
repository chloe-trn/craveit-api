using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using TasteBud.API.ViewModels.YelpViewModels;
using TasteBud.API.Services.YelpService;
using Microsoft.Extensions.Configuration;

namespace TasteBud.API.Services.YelpService
{
    public class YelpService : IYelpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public YelpService(HttpClient httpClient, IConfiguration configuration)
        {
            // Register httpClient into the class through constructor injection
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<YelpResponseViewModel> GetYelpData() // TODO: pass in quiz as a parameter and use it to build the request URL
        {
            // Build request URL 
            string location = "NYC";
            string sortBy = "best_match";
            string limit = "20";
            string URL = $"https://api.yelp.com/v3/businesses/search?location={location}&sort_by={sortBy}&limit={limit}";

           
            // Retrieve the yelp api key
            string yelpSecretApiKey = _configuration["ConnectionStrings:YelpApiSecretKey"];

            // Set the authorization header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", yelpSecretApiKey);

            // Sent a request and wait for the response
            HttpResponseMessage response = await _httpClient.GetAsync(URL);

            if (response.IsSuccessStatusCode)
            {
                // Process the successful response:

                // JSON response is returned
                string responseBody = await response.Content.ReadAsStringAsync();

                // JSON is parsed and transformed into an object of type YelpResponse
                YelpResponseViewModel yelpResponse = JsonConvert.DeserializeObject<YelpResponseViewModel>(responseBody);
                return yelpResponse;
            }
            else
            {
                // Handle the error response
                throw new Exception($"Yelp API request failed with status code: {response.StatusCode}");
            }
        }
    }
}
