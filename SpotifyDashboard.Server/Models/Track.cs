using System.Text.Json.Serialization;

namespace SpotifyDashboard.Server.Models
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]

    public class Track
    {
        [JsonPropertyName("artist")]
        public string? Artist { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("image")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("duration_ms")]
        public int Duration { get; set; }

        [JsonPropertyName("spotifyUrl")]
        public string? SpotifyUrl { get; set; }


        public Track()
        {
            
        }

        public Track(string artist, int duration, string id, string image, string name)
        {
            Artist = artist;
            Duration = duration;
            Id = id;
            ImageUrl = image;
            Name = name;
        }
    }
}