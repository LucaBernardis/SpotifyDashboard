using SpotifyDashboard.Server.Models;
using SpotifyDashboard.Server.Models.Dashboard;
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
            _httpClient.BaseAddress = new Uri("https://api.spotify.com/");

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
        public async Task<Dashboard> GetDashboardData(string token)
        {
            // General procedure to get the access token value
            var split = token.Split(' ');
            var auth = split[1];
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

            // Return Dashboard object that groups the returns of all the methods with the data i ned
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

                ArtistAlbums = artistAlbums.Select(album => new ListItem
                {
                    MainText = album.Name,
                    NumericText = album.TotalTracks,
                    Image = album.ImageUrl,
                    SpotifyUrl = album.SpotifyUrl
                }),
                NewReleases = newReleases.Select(release => new ListItem
                {
                    MainText = release.Name,
                    NumericText = release.TotalTracks,
                    Image = release.ImageUrl,
                    SpotifyUrl = release.SpotifyUrl
                }),
                RecommendedTracks = recommendedTracks.Select(recommended => new ListItem
                {
                    MainText = recommended.Name,
                    Image = recommended.ImageUrl,
                    SecondText = recommended.Artist,
                    SpotifyUrl = recommended.SpotifyUrl
                }),
                UserTopTracks = userTopTracks.Select(track => new ListItem
                {
                    MainText = track.Name,
                    Image = track.ImageUrl,
                    SecondText = track.Artist,
                    NumericText = track.Duration
                }),
                UserPlaylists = userPlaylists.Select(playlist => new ListItem
                {
                    MainText = playlist.Name,
                    SecondText = playlist.Owner,
                    Image = playlist.Image,
                    SpotifyUrl = playlist.SpotifyUrl
                })
            };
        }
    }
}
