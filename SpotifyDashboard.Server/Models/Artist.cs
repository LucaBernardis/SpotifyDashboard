using System.Text.Json.Serialization;

namespace SpotifyDashboard.Server.Models
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]
    public class Artist
    {
        [JsonPropertyName("genre")]
        public string? Genres { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("image")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        public Artist()
        {
            
        }

        public Artist(string genre, string id, string image, string name)
        {
            Genres = genre;
            Id = id;
            ImageUrl = image;
            Name = name;
        }
    }
}
