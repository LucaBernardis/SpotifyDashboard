using System.Text.Json.Serialization;
using ThirdParty.Json.LitJson;

namespace SpotifyDashboard.Server.Models
{
    public class ListItem
    {
        public string? Image { get; set; }

        public string? MainText { get; set; }

        public string? SecondText { get; set; }

        public string? SpotifyUrl { get; set; }

        public int? NumericText { get; set; }
    }
}
