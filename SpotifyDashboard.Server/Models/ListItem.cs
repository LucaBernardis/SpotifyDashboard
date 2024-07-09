namespace SpotifyDashboard.Server.Models
{

    /// <summary>
    /// Represent the list object
    /// </summary>
    public class ListItem
    {
        public string Image { get; set; } = "";

        public string MainText { get; set; } = "";

        public string SecondText { get; set; } = "";

        public string SpotifyUrl { get; set; } = "";
    }
}
