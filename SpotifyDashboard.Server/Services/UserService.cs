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

            var split = token.Split(' ');
            var auth = split[1];
            _httpClient.BaseAddress = new Uri("https://api.spotify.com/");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            using HttpResponseMessage response = await _httpClient.GetAsync("v1/me");

            response.EnsureSuccessStatusCode(); // Throw an exception if the response is not successful

            var responseBody = await response.Content.ReadAsStringAsync();
            var userData = JsonSerializer.Deserialize<User>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return userData;
        }
    }
}
