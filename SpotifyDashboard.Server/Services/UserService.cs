using SpotifyDashboard.Server.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;

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

            // General procedure to get the access token value
            var split = token.Split(' ');
            var auth = split[1];

            _httpClient.BaseAddress = new Uri("https://api.spotify.com/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);

            // Http call to the spotify api address
            using HttpResponseMessage response = await _httpClient.GetAsync("v1/me");

            response.EnsureSuccessStatusCode(); // Throw an exception if the response is not successful

            var responseBody = await response.Content.ReadAsStringAsync();

            // Retrieving the image data from the json Object
            var jObj = JsonNode.Parse(responseBody)?.AsObject();
            var images = jObj["images"]?.AsArray();

            var userData = JsonSerializer.Deserialize<User>(responseBody);

            // Setting the imageUrl property to the image array url value
            userData.Imageurl = images[0]["url"]?.ToString();

            return userData;
        }

        public async Task<IEnumerable<Playlist>> GetUserPlaylist(string token)
        {

            // General procedure to get the access token value
            var split = token.Split(' ');
            var auth = split[1];

            _httpClient.BaseAddress = new Uri("https://api.spotify.com/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);

            // Http call to the spotify api address
            using HttpResponseMessage response = await _httpClient.GetAsync($"v1/me/playlists?limit=10");

            response.EnsureSuccessStatusCode(); // Throw an exception if the response is not successful

            var responseBody = await response.Content.ReadAsStringAsync();

            // Parsing the item object in json to access its properties
            var jObj = JsonNode.Parse(responseBody)?.AsObject();
            var items = jObj["items"]?.AsArray();

            var playlists = JsonSerializer.Deserialize<List<Playlist>>(items.ToJsonString());

            // For each playlist object
            for (int i = 0; i < playlists?.Count; i++)
            {
                var playlist = playlists[i];
                var item = items[i];

                // Assign to the Owner property the value of the owner display name
                var owner = item["owner"].AsObject();
                if (owner.Count > 0)
                {
                    playlist.Owner = owner["display_name"]?.ToString();
                }

                // Assign to the SpotifyUrl property the value of the external spotify link
                var exturl = item["external_urls"]?.AsObject();
                playlist.SpotifyUrl = exturl["spotify"]?.ToString();

                // Assign to the ImageUrl property the value of the image url
                var image = item["images"].AsArray()[0]?.AsObject();
                playlist.ImageUrl = image["url"]?.ToString();

            }

            return playlists;

        }
    }
}
