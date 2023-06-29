using System.Net.Http.Headers;
using Newtonsoft.Json;
using CraveIt.API.Models;
using CraveIt.API.ViewModels;

namespace CraveIt.API.Services.YelpService
{
    public class YelpService : IYelpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public YelpService(HttpClient httpClient, IConfiguration configuration)
        {
            // Register httpClient through constructor injection
            _httpClient = httpClient;
            _configuration = configuration;
        }

        // Perform Yelp API call
        public async Task<YelpResponseViewModel> GetYelpData(Quiz quiz)
        {
            // Define request URL parameters
            string location = quiz.Location;
            string distance = quiz.Distance;
            string priceRange = quiz.PriceRange;
            string cuisine = quiz.Cuisine;
            string sortBy = "best_match";
            string limit = "50";

            // Split the comma-separated values into an array
            string[] priceRangeValues = priceRange.Split(',');
            string[] cuisineValues = cuisine.Split(',');

            // Build initial request URL
            string URL = $"https://api.yelp.com/v3/businesses/search?location={location}&radius={distance}&sort_by={sortBy}&limit={limit}";

            // Append price ranges to URL
            foreach (string price in priceRangeValues)
            {
                URL += $"&price={price}";
            }

            // Append cuisine types to URL
            foreach (string category in cuisineValues)
            {
                URL += $"&categories={category}";
            }

            // Retrieve the Yelp API key
            string yelpSecretApiKey = _configuration["ConnectionStrings:YelpApiSecretKey"];

            // Set the authorization header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", yelpSecretApiKey);

            // Sent a request and wait for the response
            HttpResponseMessage response = await _httpClient.GetAsync(URL);

            if (response.IsSuccessStatusCode) // Process the successful response
            {
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
