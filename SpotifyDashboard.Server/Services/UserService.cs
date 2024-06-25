using SpotifyDashboard.Server.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SpotifyDashboard.Server.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<User> GetUserData(string token)
        {
            _httpClient.BaseAddress = new Uri("https://api.spotify.com/v1");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "token");
            using HttpResponseMessage response = await _httpClient.GetAsync("me");

            response.EnsureSuccessStatusCode(); // Throw an exception if the response is not successful

            var responseBody = await response.Content.ReadAsStringAsync();
            var userData = JsonSerializer.Deserialize<User>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return userData;
        }
    }
}
