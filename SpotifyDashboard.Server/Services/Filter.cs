using SpotifyDashboard.Server.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SpotifyDashboard.Server.Services
{
    public static class Filter
    {
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
