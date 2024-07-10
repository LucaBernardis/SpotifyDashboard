using SpotifyDashboard.Server.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SpotifyDashboard.Server.Services
{
    /// <summary>
    /// Provide the methods to filter the <see cref="JsonSerializer.Deserialize{TValue}(string, JsonSerializerOptions?)"/> response object
    /// </summary>
    public static class Filter
    {

        /// <summary>
        /// Filters the <see cref="JsonSerializer.Deserialize{TValue}(string, JsonSerializerOptions?)"/> response and fill the <see cref="Album"/> data taking only the needed ones
        /// </summary>
        /// <param name="jsonNode"> The Json Node to deserialize </param>
        /// <param name="param"> The object of the Json Node to deserialize </param>
        /// <returns> A <see cref="List{T}"/> of filtered <see cref="Album"/> objects with only the necessary data </returns>
        public static List<Album> MapAlbums(JsonNode jsonNode, string param)
        {

            var albumsJson = new JsonArray();

            if (param == "items")
                albumsJson = jsonNode?[param]?.AsArray();
            else
            {
                var wrapper = jsonNode?[param]?.AsObject();
                albumsJson = wrapper?["items"]?.AsArray();
            }

            var albumList = JsonSerializer.Deserialize<List<Album>>(albumsJson!.ToJsonString());

            for (int i = 0; i < albumList!.Count; i++)
            {
                var album = albumList[i];
                var item = albumsJson[i];

                if(item != null)
                {
                    album.SpotifyUrl = item["external_urls"]?.AsObject()?["spotify"]?.ToString();
                    album.ImageUrl = item["images"]?.AsArray()[0]?["url"]?.ToString();
                    album.Artist = item["artists"]?.AsArray()[0]?["name"]?.ToString();
                }  
            }

            return albumList;
        }


        /// <summary>
        /// Filters the <see cref="JsonSerializer.Deserialize{TValue}(string, JsonSerializerOptions?)"/> response and fill the <see cref="Track"/> data taking only the needed ones
        /// </summary>
        /// <param name="jsonNode"> The Json Node to deserialize </param>
        /// <param name="param"> The object of the Json Node to deserialize </param>
        /// <returns> A <see cref="List{T}"/> of filtered <see cref="Track"/> objects with only the necessary data</returns>
        public static List<Track> MapTracks(JsonNode jsonNode, string param)
        {
            var tracksJson = jsonNode?[param]?.AsArray();
            var tracks = JsonSerializer.Deserialize<List<Track>>(tracksJson!.ToJsonString());

            for (int i = 0; i < tracks!.Count; i++)
            {
                var track = tracks[i];
                var item = tracksJson[i];

                if(item != null)
                {
                    track.Artist = item["artists"]?.AsArray()[0]?["name"]?.ToString();
                    track.ImageUrl = item["album"]?.AsObject()?["images"]?.AsArray()[0]?["url"]?.ToString();
                    track.SpotifyUrl = item["external_urls"]?.AsObject()?["spotify"]?.ToString();
                }
                
            }

            return tracks;
        }
    }
}
