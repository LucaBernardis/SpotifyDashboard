using SpotifyDashboard.Server.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SpotifyDashboard.Server.Services
{
    public partial class DashboardService
    {

        /// <summary>
        /// Method to get the top tracks of the current authenticated user
        /// </summary>
        /// <returns> A list of the user's favourite tracks with their data </returns>
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
        /// Method to get a list of recommended tracks basing the request on the favourite artist, his genre and his best song
        /// ( U can choose any artist, song and genre. This implementation take this parameters 
        /// just because its easier to manage with the other existing api calls )
        /// </summary>
        /// <param name="seedArtist"> The query parameter that containes the artist id value </param>
        /// <param name="seedGenres"> The query parameter that contains the artist main genre </param>
        /// <param name="seedTrack"> The query parameter that contains the track id value </param>
        /// <returns></returns>
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