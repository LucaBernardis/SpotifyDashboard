namespace SpotifyDashboard.Server.Models
{

    /// <summary>
    /// Represent the list object, all the objects have
    /// the same similar structure so you can group them in a generic class
    /// </summary>
    public class ListItem
    {
        public string Image { get; set; } = "";

        public string MainText { get; set; } = "";

        public string SecondText { get; set; } = "";

        public string SpotifyUrl { get; set; } = "";
    }
}
