using SpotifyDashboard.Server.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SpotifyDashboard.Server.Services
{
    public partial class DashboardService
    {

        /// <summary>
        /// Method to get all the data related to the current authenticated user
        /// </summary>
        /// <returns> A User object with the values usefull to the dashboard component </returns>
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
            userData!.ImageUrl = images?[0]?["url"]?.ToString();

            return userData;
        }

        /// <summary>
        /// Method to get all the user's playlists and the related data
        /// </summary>
        /// <returns> A list of playlists with all their data </returns>
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

            // For each playlist object
            for (int i = 0; i < playlists?.Count; i++)
            {
                var playlist = playlists[i];
                var item = items[i];

                // Assign to the Owner property the value of the owner display name
                var owner = item?["owner"]?.AsObject();
                if (owner!.Count > 0)
                {
                    playlist.Owner = owner["display_name"].ToString();
                }

                // Assign to the SpotifyUrl property the value of the external spotify link
                var exturl = item? ["external_urls"]?.AsObject();
                if(exturl != null)
                    playlist.SpotifyUrl = exturl["spotify"].ToString();

                // Assign to the ImageUrl property the value of the image url
                var image = item?["images"]?.AsArray()[0]?.AsObject();

                if(image != null)
                    playlist.Image = image["url"].ToString();

            }

            return playlists!;

        }
    }
}
