using SpotifyDashboard.Server.Models;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace SpotifyDashboard.Server.Services
{
    public partial class DashboardService
    {
        /// <summary>
        /// Retrieve the user's favourite artist 
        /// </summary>
        /// <returns> An <see cref="Artist"/> object with the usefull data about the user's favourite artist </returns>
        public async Task<Artist> GetTopArtist()
        {
            // Http call to the spotify api address
            using HttpResponseMessage response = await _httpClient.GetAsync("v1/me/top/artists?limit=1");
            response.EnsureSuccessStatusCode(); // Throw an exception if the response is not successful

            var responseBody = await response.Content.ReadAsStringAsync();
            var jObj = JsonNode.Parse(responseBody)?.AsObject();
            var items = jObj?["items"]?.AsArray();

            var artistJson = items?[0]?.ToJsonString();
            var artist = JsonSerializer.Deserialize<Artist>(artistJson!);

            // Assign to the Genres property the first value of the genres array
            var genres = items?[0]?["genres"]?.AsArray();

            if(genres != null)
                artist!.Genres = genres[0].ToString();

            // Assign to the ImageUrl property the value of the image array url
            if (artist != null)
            {
                var images = jObj?["items"]?[0]?["images"]?.AsArray();
                artist.ImageUrl = images!.First()?["url"]?.ToString();
            }

            return artist!;
        }

        /// <summary>
        /// Retrieve the most famous track of the user's favourite artist <seealso cref="GetTopArtist"/>
        /// </summary>
        /// <param name="artistId"> The id of the artist you need to pass in the api call to get its related most famouse song </param>
        /// <returns> A <see cref="Track"/> object with the artist's most famous track data </returns>
        public async Task<Track> GetArtistTopTrack(string artistId)
        {
            // Http call to the spotify api address
            using HttpResponseMessage response = await _httpClient.GetAsync($"/v1/artists/{artistId}/top-tracks");

            response.EnsureSuccessStatusCode(); // Throw an exception if the response is not successful

            var responseBody = await response.Content.ReadAsStringAsync();

            var jObj = JsonNode.Parse(responseBody)?.AsObject();
            var topTrack = Filter.MapTracks(jObj!, "tracks");
            return topTrack[0];
        }

        /// <summary>
        /// Retrieve all the albums the artist made or is part of
        /// </summary>
        /// <param name="artistId"> The id of the artist you need to pass in the api call to get all its related albums </param>
        /// <returns> A <see cref="List{T}"/> of the <see cref="Album"/> the artist made or is part of and the related usefull data </returns>
        public async Task<IEnumerable<Album>> GetArtistAlbums(string artistId)
        {
            // Http call to the spotify api address
            using HttpResponseMessage response = await _httpClient.GetAsync($"/v1/artists/{artistId}/albums");

            response.EnsureSuccessStatusCode(); // Throw an exception if the response is not successful

            var responseBody = await response.Content.ReadAsStringAsync();

            var jObj = JsonNode.Parse(responseBody)?.AsObject();
            var albumList = Filter.MapAlbums(jObj!, "items");

            return albumList;

        }

        /// <summary>
        /// Method to retrieve spotify new releases, its a list of albums with all the related data, 
        /// like total tracks, data about the artist and external links to the spotify page
        /// </summary>
        /// <returns> A <see cref="List{T}"/> of <see cref="Album"/> </returns>
        public async Task<IEnumerable<Album>> GetNewReleases()
        {
            //// Http call to the spotify api address
            using HttpResponseMessage response = await _httpClient.GetAsync($"/v1/browse/new-releases");

            response.EnsureSuccessStatusCode(); // Throw an exception if the response is not successful

            var responseBody = await response.Content.ReadAsStringAsync();

            var jObj = JsonNode.Parse(responseBody)?.AsObject();
            var newReleases = Filter.MapAlbums(jObj!, "albums");

            return newReleases;

        }

    }
}
