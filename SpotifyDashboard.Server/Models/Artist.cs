using System.Text.Json.Serialization;

namespace SpotifyDashboard.Server.Models
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]
    public class Artist
    {
        [JsonPropertyName("genre")]
        public string Genre { get; set; } = "";

        [JsonPropertyName("id")]
        public string Id { get; set; } = "";

        [JsonPropertyName("image")]
        public string ImageUrl { get; set; } = "";

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        public Artist()
        {
            
        }

        // Constructor for test method
        public Artist(string genre, string id, string image, string name)
        {
            Genre = genre;
            Id = id;
            ImageUrl = image;
            Name = name;
        }
    }
}
