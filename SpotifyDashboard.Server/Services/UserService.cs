using SpotifyDashboard.Server.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SpotifyDashboard.Server.Services
{
    public partial class DashboardService
    {

        /// <summary>
        /// Method to get all the data related to the current authenticated user
        /// </summary>
        /// <returns> A <see cref="User"/> object </returns>
        public async Task<User> GetUserData()
        {
            // Http call to the spotify api address
            using HttpResponseMessage response = await _httpClient.GetAsync("v1/me");

            response.EnsureSuccessStatusCode(); // Throw an exception if the response is not successful

            var responseBody = await response.Content.ReadAsStringAsync();

            // Retrieving the image data from the json Object
            var jObj = JsonNode.Parse(responseBody)?.AsObject();
            var images = jObj?["images"]?.AsArray();

            var userData = JsonSerializer.Deserialize<User>(responseBody);

            // Setting the imageUrl property to the image array url value
            userData!.ImageUrl = images![0]!["url"]!.ToString();

            return userData;
        }

        /// <summary>
        /// Method to get all the user's <see cref="Playlist"> and the related data
        /// </summary>
        /// <returns> A <see cref="List{T}"/> of <see cref="Playlist"/> </returns>
        public async Task<IEnumerable<Playlist>> GetUserPlaylist()
        {
            // Http call to the spotify api address
            using HttpResponseMessage response = await _httpClient.GetAsync($"v1/me/playlists?limit=20");

            response.EnsureSuccessStatusCode(); // Throw an exception if the response is not successful

            var responseBody = await response.Content.ReadAsStringAsync();

            // Parsing the item object in json to access its properties
            var jObj = JsonNode.Parse(responseBody)?.AsObject();
            var items = jObj?["items"]?.AsArray();

            var playlists = JsonSerializer.Deserialize<List<Playlist>>(items!.ToJsonString());

            for (int i = 0; i < playlists?.Count; i++)
            {
                var playlist = playlists[i];
                var item = items[i];

                if(item != null)
                {
                    playlist.Owner = item["owner"]!.AsObject()!["display_name"]!.ToString();
                    playlist.Image = item["images"]!.AsArray()[0]!.AsObject()!["url"]!.ToString();
                    playlist.SpotifyUrl = item["external_urls"]!.AsObject()!["spotify"]!.ToString();
                }

            }

            return playlists!;

        }
    }
}
