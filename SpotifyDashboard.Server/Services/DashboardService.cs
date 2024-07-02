using SpotifyDashboard.Server.Models;
using System.Net.Http.Headers;

namespace SpotifyDashboard.Server.Services
{
    // Passo 2
    public partial class DashboardService
    {
        private readonly HttpClient _httpClient;

        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// This method has the task of retrieving all the data from the other services
        /// and combine the results in a single object that contains for each method only
        /// the data usefull to make the angular component work properly
        /// </summary>
        /// <param name="token">
        /// The access_token value retrieved after the authentication 
        /// from the angular page, without it you are unauthorized to make any api call
        /// </param>
        /// <returns> An object that contains the result of all the usefull data from the other sevrices methods </returns>
        public async Task<object> GetDashboardData(string token)
        {
            // General procedure to get the access token value
            var split = token.Split(' ');
            var auth = split[1];
            _httpClient.BaseAddress = new Uri("https://api.spotify.com/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);



            // Artist service methods
            var topArtist = await GetTopArtist();
            var topArtistTopTrack = await GetArtistTopTrack(topArtist.Id);
            var artistAlbums = await GetAlbums(topArtist.Id); // IEnumerable
            var newReleases = await GetNewReleases(); // IEnumerable

            // User service methods
            var user = await GetUserData();
            var userPlaylists = await GetUserPlaylist(); // IEnumerable

            // Track service methods
            var userTopTracks = await GetTopTenSongs(); // IEnumerable
            var recommendedTracks = await GetRecommendedSongs(topArtist.Id, topArtist.Genres, topArtistTopTrack.Id); // IEnumerable

            // Return object that group the returns of all the methods
            return new Dashboard
            {
                User = new User
                {
                    DisplayName = user.DisplayName,
                    ImageUrl = user.ImageUrl,
                    Id = user.Id,
                },

                TopArtist = new Artist
                {
                    Name = topArtist.Name,
                    ImageUrl = topArtist.ImageUrl
                },
                ArtistTopTrack = new Track
                {
                    Name = topArtistTopTrack.Name,
                    ImageUrl = topArtistTopTrack.ImageUrl,
                    Artist = topArtistTopTrack.Artist
                },

                ArtistAlbums = artistAlbums.Select(album => new Album
                {
                    Name = album.Name,
                    ImageUrl = album.ImageUrl,
                    TotalTracks = album.TotalTracks,
                    SpotifyUrl = album.SpotifyUrl,
                }),
                NewReleases = newReleases.Select(release => new Album
                {
                    Name = release.Name,
                    ImageUrl = release.ImageUrl,
                    TotalTracks = release.TotalTracks,
                    SpotifyUrl = release.SpotifyUrl
                }),
                RecommendedTracks = recommendedTracks.Select(recommended => new Track
                {
                    Name = recommended.Name,
                    ImageUrl = recommended.ImageUrl,
                    Artist = recommended.Artist,
                    SpotifyUrl = recommended.SpotifyUrl
                }),
                UserTopTracks = userTopTracks.Select(track => new Track
                {
                    Name = track.Name,
                    ImageUrl = track.ImageUrl,
                    Artist = track.Artist,
                    Duration = track.Duration
                }),
                UserPlaylists = userPlaylists.Select(playlist => new Playlist
                {
                    Name = playlist.Name,
                    Owner = playlist.Owner,
                    Image = playlist.Image,
                    SpotifyUrl = playlist.SpotifyUrl
                })
            };
        }
    }
}
