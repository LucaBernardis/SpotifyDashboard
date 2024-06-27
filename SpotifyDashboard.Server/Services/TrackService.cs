using SpotifyDashboard.Server.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SpotifyDashboard.Server.Services
{
    public class TrackService
    {
        private readonly HttpClient _httpClient;

        public TrackService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Track>> GetTopTenSongs(string token)
        {
            var split = token.Split(' ');
            var auth = split[1];

            _httpClient.BaseAddress = new Uri("https://api.spotify.com/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            using HttpResponseMessage response = await _httpClient.GetAsync("v1/me/top/tracks");

            var responseBody = await response.Content.ReadAsStringAsync();

            var jObj = JsonNode.Parse(responseBody).AsObject();
            var items = jObj["items"].AsArray();

            var tracks = JsonSerializer.Deserialize<List<Track>>(items.ToJsonString());

            for (int i = 0; i < tracks.Count; i++)
            {
                var track = tracks[i];
                var item = items[i];
                var artists = item["artists"].AsArray();
                if (artists.Count > 0)
                {
                    track.Artist = artists[0]["name"]?.ToString();
                }

                var album = item["album"].AsObject();
                var images = album["images"].AsArray();
                if (images.Count > 0)
                {
                    track.ImageUrl = images[0]["url"]?.ToString();
                }
            }

            return tracks;
        }
    }
}