namespace SpotifyDashboard.Server.Models
{
    public class Track
    {
        public Artist Artist { get; set; };
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string[] Genre { get; set; }
        public int Id { get; set; }

        public Track()
        {
            Artist = new Artist();
            Name = string.Empty;
            ImageUrl = string.Empty;
            Genre = []; 
        }
    }
}
