using System.Text.Json.Serialization;

namespace SpotifyDashboard.Server.Models
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]
    public class Playlist
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("image")]
        public string Image { get; set; } = "";

        public string SpotifyUrl { get; set; } = "";

        public string Owner { get; set; } = "";

    }
}
