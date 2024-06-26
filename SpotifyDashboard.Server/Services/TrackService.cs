
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

            response.EnsureSuccessStatusCode(); // Throw an exception if the response is not successful

            var responseBody = await response.Content.ReadAsStringAsync();

            var jObj = JsonNode.Parse(responseBody).AsObject();
            var items = jObj["items"].AsArray();

            var tracks = JsonSerializer.Deserialize<List<Track>>(items);


            //foreach(var track in tracks)
            //{
            //    foreach (var item in items)
            //    {
            //        var artistsName = items["artist"]["name"].ToString;
            //        track.Artists = artistsName.ToString();
            //    }
            //}
            

            return tracks;
        }
    }
}
