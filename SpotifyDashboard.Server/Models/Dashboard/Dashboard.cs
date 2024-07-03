namespace SpotifyDashboard.Server.Models.Dashboard
{
    public class Dashboard
    {
        public User User { get; set; }

        public Artist TopArtist { get; set; }
        public Track ArtistTopTrack { get; set; }

        public IEnumerable<Album> ArtistAlbums { get; set; }
        public IEnumerable<Album> NewReleases { get; set; }
        public IEnumerable<Track> RecommendedTracks { get; set; }
        public IEnumerable<Track> UserTopTracks { get; set; }
        public IEnumerable<Playlist> UserPlaylists { get; set; }
    }
}
