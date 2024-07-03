namespace SpotifyDashboard.Server.Models.Dashboard
{
    public class Dashboard
    {
        public User User { get; set; }

        public Artist TopArtist { get; set; }
        public Track ArtistTopTrack { get; set; }

        public IEnumerable<ListItem> ArtistAlbums { get; set; }
        public IEnumerable<ListItem> NewReleases { get; set; }
        public IEnumerable<ListItem> RecommendedTracks { get; set; }
        public IEnumerable<ListItem> UserTopTracks { get; set; }
        public IEnumerable<ListItem> UserPlaylists { get; set; }
    }
}
