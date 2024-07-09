using SpotifyDashboard.Server.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SpotifyDashboard.Server.Services
{
    public static class Filter
    {

        /// <summary>
        /// Method to filter the Deserialization response and filter the albums data taking only the usefull ones
        /// </summary>
        /// <param name="jsonNode"> The Json Node to desesialize </param>
        /// <param name="param"> The element of the Json Node to deserialize </param>
        /// <returns></returns>
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
        /// Method to filter the Deserialization response and filter the tracks data taking only the usefull ones
        /// </summary>
        /// <param name="jsonNode"> The Json Node to desesialize </param>
        /// <param name="param"> The element of the Json Node to deserialize </param>
        /// <returns></returns>
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
