using SpotifyDashboard.Server.Models;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace SpotifyDashboard.Server.Services
{
    public class ArtistService
    {

        private readonly HttpClient _httpClient;

        public ArtistService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Artist> GetTopArtist(string token)
        {
            var split = token.Split(' ');
            var auth = split[1];

            _httpClient.BaseAddress = new Uri("https://api.spotify.com/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            using HttpResponseMessage response = await _httpClient.GetAsync("v1/me/top/artists?limit=1");

            response.EnsureSuccessStatusCode(); // Throw an exception if the response is not successful

            var responseBody = await response.Content.ReadAsStringAsync();

            var jObj = JsonNode.Parse(responseBody).AsObject();
            var items = jObj["items"].AsArray();

            var artistJson = items[0].ToJsonString();
            var artist = JsonSerializer.Deserialize<Artist>(artistJson);

            if (artist != null)
            {
                var images = jObj["items"][0]["images"].AsArray();
                artist.ImageUrl = images.FirstOrDefault()?["url"]?.ToString();
            }

            return artist;
        }

    }
}
