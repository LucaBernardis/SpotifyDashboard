namespace SpotifyDashboard.Server.Models.Dashboard
{
    /// <summary>
    /// Object that groups all the <see cref="WidgetComponent"/> in the dashboard
    /// </summary>
    public class Dashboard
    {
        public User User { get; set; } = new User();

        public Artist TopArtist { get; set; } = new Artist();
        public Track ArtistTopTrack { get; set; } = new Track();

        public IEnumerable<ListItem> ArtistAlbums { get; set; } = [];
        public IEnumerable<ListItem> NewReleases { get; set; } = [];
        public IEnumerable<ListItem> RecommendedTracks { get; set; } = [];
        public IEnumerable<ListItem> UserTopTracks { get; set; } = [];
        public IEnumerable<ListItem> UserPlaylists { get; set; } = [];

        public Dashboard()
        {
            
        }
    }
}
