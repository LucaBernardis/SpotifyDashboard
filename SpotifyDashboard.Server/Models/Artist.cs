using System.Text.Json.Serialization;

namespace SpotifyDashboard.Server.Models
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]
    public class Artist
    {
        //public string[] Genres { get; set; }
        //public int Id { get; set; }
        //public string? ImageUrl { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

    }
}
