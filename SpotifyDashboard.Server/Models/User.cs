using System.Text.Json.Serialization;

namespace SpotifyDashboard.Server.Models
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]

    public class User
    {
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = "";

        [JsonPropertyName("email")]
        public string Email { get; set; } = "";

        [JsonPropertyName("id")]
        public string Id { get; set; } = "";

        [JsonPropertyName("image")]
        public string? ImageUrl { get; set; }

        public User(string name, string email, string id, string image)
        {
            DisplayName = name;
            Email = email;
            Id = id;
            ImageUrl = image;
        }

        public User()
        {
            
        }
    }

}
