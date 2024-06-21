namespace SpotifyDashboard.Server.Models
{
    public class Artist
    {
        public string[] Genres { get; set; }
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string Name { get; set; }

        public Artist()
        {
            Genres = [];
            ImageUrl = string.Empty;
            Name = string.Empty;
        }
    }
}
