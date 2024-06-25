using SpotifyDashboard.Server.Models;

namespace SpotifyDashboard.Server.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<User> GetUserData()
        {
            _httpClient.BaseAddress = new Uri("https://api.spotify.com/v1/");
            using HttpResponseMessage repsonse = await _httpClient.GetAsync("me");

            Console.WriteLine(repsonse.Content);

           return new User();
        }
    }
}
