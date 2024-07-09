using System.Text.Json.Serialization;

namespace SpotifyDashboard.Server.Models
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]

    public class Track
    {
        [JsonPropertyName("artist")]
        public string Artist { get; set; } = "";

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("id")]
        public string Id { get; set; } = "";

        [JsonPropertyName("image")]
        public string ImageUrl { get; set; } = "";

        [JsonPropertyName("spotifyUrl")]
        public string SpotifyUrl { get; set; } = "";


        public Track()
        {
            
        }

        public Track(string artist, string id, string image, string name)
        {
            Artist = artist;
            Id = id;
            ImageUrl = image;
            Name = name;
        }
    }
}