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

            var genres = items[0]["genres"].AsArray();
            artist.Genres = genres[0]?.ToString();

            if (artist != null)
            {
                var images = jObj["items"][0]["images"].AsArray();
                artist.ImageUrl = images.FirstOrDefault()?["url"]?.ToString();
            }

            return artist;
        }

        public async Task<Track> GetArtistTopTrack(string token, string artistId)
        {
            var split = token.Split(' ');
            var auth = split[1];

            _httpClient.BaseAddress = new Uri("https://api.spotify.com/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            using HttpResponseMessage response = await _httpClient.GetAsync($"/v1/artists/{artistId}/top-tracks");

            response.EnsureSuccessStatusCode(); // Throw an exception if the response is not successful

            var responseBody = await response.Content.ReadAsStringAsync();

            var jObj = JsonNode.Parse(responseBody).AsObject();
            var tracks = jObj["tracks"]?.AsArray();

            var track = JsonSerializer.Deserialize<List<Track>>(tracks.ToJsonString());

            var album = tracks[0]["album"]?.AsObject();
            var albumImg = album["images"]?.AsArray();
            track[0].ImageUrl = albumImg[0]["url"]?.ToString();

            track[0].Name = tracks[0]["name"]?.ToString();

            var artist = tracks[0]["artists"].AsArray();
            track[0].Artist = artist[0]["name"]?.ToString();

            var topTrack = track[0];
            return topTrack;
        }

        public async Task<IEnumerable<Album>> GetAlbums(string token, string artistId)
        {
            var split = token.Split(' ');
            var auth = split[1];

            _httpClient.BaseAddress = new Uri("https://api.spotify.com/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            using HttpResponseMessage response = await _httpClient.GetAsync($"/v1/artists/{artistId}/albums");

            response.EnsureSuccessStatusCode(); // Throw an exception if the response is not successful

            var responseBody = await response.Content.ReadAsStringAsync();

            var jObj = JsonNode.Parse(responseBody)?.AsObject();
            var albums = jObj["items"]?.AsArray();


            var albumList = JsonSerializer.Deserialize<List<Album>>(albums.ToJsonString());

            for(int i = 0; i < albumList.Count; i++)
            {
                var album = albumList[i];
                var item = albums[i];

                var extUrl = item["external_urls"]?.AsObject();
                album.SpotifyUrl = extUrl["spotify"]?.ToString();

                var image = item["images"]?.AsArray();
                var imageUrl = image[0]["url"]?.ToString();
                album.ImageUrl = imageUrl;

                var artist = item["artists"]?.AsArray();
                var artistName = artist[0]["name"]?.ToString();
                album.Artist = artistName;
            }

            return albumList;

        }

    }
}
