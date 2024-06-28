using System.Text.Json.Serialization;

namespace SpotifyDashboard.Server.Models
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]
    public class Album
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("image")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("artist")]
        public string? Artist { get; set; }

        [JsonPropertyName("spotifyUrl")]
        public string? SpotifyUrl { get; set; }

        [JsonPropertyName("total_tracks")]
        public int TotalTracks { get; set; }

    }
}
