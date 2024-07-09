using SpotifyDashboard.Server.Models;
using System.Text.Json.Nodes;

namespace SpotifyDashboard.Server.Services
{
    public partial class DashboardService
    {

        /// <summary>
        /// Method to get the top <see cref="Track"/>s of the current authenticated user
        /// </summary>
        /// <returns> A <see cref="List{T}"/> of the user's favourite <see cref="Track"/> with their data </returns>
        public async Task<IEnumerable<Track>> GetTopTenSongs()
        {
            // Http call to the spotify api address
            using HttpResponseMessage response = await _httpClient.GetAsync("v1/me/top/tracks");

            var responseBody = await response.Content.ReadAsStringAsync();

            // Retrieving the items from the json Object
            var jObj = JsonNode.Parse(responseBody)?.AsObject();
            var tracks = Filter.MapTracks(jObj!, "items");

            return tracks;
        }

        /// <summary>
        /// Method to get a list of recommended <see cref="Track"/> basing the request on the favourite <see cref="Artist"/>, his genre and his best song
        /// </summary>
        /// <param name="seedArtist"> The query parameter that containes the <see cref="Artist"/> id value </param>
        /// <param name="seedGenres"> The query parameter that contains the <see cref="Artist"/> main genre </param>
        /// <param name="seedTrack"> The query parameter that contains the <see cref="Track"/> id value </param>
        /// <returns> a <see cref="List{T}"/> of recommended <see cref="Track"/> </returns>
        public async Task<IEnumerable<Track>> GetRecommendedSongs(string seedArtist, string seedGenres, string seedTrack)
        {
            // Building the query parameter with the values passed in the request call parameters
            var queryParams = $"seed_artists={seedArtist}&seed_genres={Uri.EscapeDataString(seedGenres)}&seed_tracks={seedTrack}";

            // Http call to the spotify api address
            using HttpResponseMessage response = await _httpClient.GetAsync($"v1/recommendations?{queryParams}");

            var responseBody = await response.Content.ReadAsStringAsync();

            // Retrieving the tracks from the json Object
            var jObj = JsonNode.Parse(responseBody)?.AsObject();
            var recommended = Filter.MapTracks(jObj!, "tracks");

            return recommended!;
        }
    }
}