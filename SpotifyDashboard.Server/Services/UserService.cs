using SpotifyDashboard.Server.Models;
using System.Net.Http.Headers;

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
            _httpClient.BaseAddress = new Uri("https://api.spotify.com/v1/");
            using HttpResponseMessage response = await _httpClient.GetAsync("me");

            var splitted = token.Split(' ');

            var type = splitted[0];
            var auth = splitted[1];

            response.Headers.Add(type, auth);
            Console.WriteLine(response.Content.ToString);

           return new User("Luca", auth);
        }
    }
}
