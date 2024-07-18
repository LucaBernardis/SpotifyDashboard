using SpotifyDashboard.Server.Models;
using SpotifyDashboard.Server.Models.Dashboard;
using System.Net.Http.Headers;

namespace SpotifyDashboard.Server.Services
{
    /// <summary>
    /// <para>Provide the method <see cref="GetDashboardData(string)"/> to gather the data retrieved by the api calls</para>
    /// <remarks>All the other services excepted <see cref="ConfigService"/> are a partial class of this service</remarks>
    /// </summary>
    public partial class DashboardService
    {
        private readonly HttpClient _httpClient;
        private readonly string SpotifyApi = "https://api.spotify.com/";

        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(SpotifyApi);
        }

        /// <summary>
        /// Retrieve all the data from the services and combine the results in a single <see cref="Dashboard"/> object
        /// </summary>
        /// <param name="token">
        /// The access_token value retrieved after the authentication from the angular page, without it you are unauthorized to make any api call
        /// </param>
        /// <returns> A <see cref="Dashboard"/> object </returns>
        public async Task<Dashboard> GetDashboardData(string token)
        {
            // General procedure to get the access token value
            var split = token.Split(' ');
            var auth = split[1];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);

            // Artist service methods
            var topArtist = await GetTopArtist();
            var topArtistTopTrack = await GetArtistTopTrack(topArtist.Id);
            var artistAlbums = await GetArtistAlbums(topArtist.Id); // IEnumerable
            var newReleases = await GetNewReleases(); // IEnumerable

            // User service methods
            var user = await GetUserData();
            var userPlaylists = await GetUserPlaylist(); // IEnumerable

            // Track service methods
            var userTopTracks = await GetTopTracks(); // IEnumerable
            var recommendedTracks = await GetRecommendedTracks(topArtist.Id, topArtist.Genre, topArtistTopTrack.Id); // IEnumerable

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
                    SecondText = album.TotalTracks.ToString(),
                    Image = album.ImageUrl,
                    SpotifyUrl = album.SpotifyUrl
                }),
                NewReleases = newReleases.Select(release => new ListItem
                {
                    MainText = release.Name,
                    SecondText = $"{release.TotalTracks} tracks",
                    Image = release.ImageUrl,
                    SpotifyUrl = release.SpotifyUrl
                }),
                RecommendedTracks = recommendedTracks.Select(recommended => new ListItem
                {
                    MainText = recommended.Name,
                    SecondText = recommended.Artist,
                    Image = recommended.ImageUrl,
                    SpotifyUrl = recommended.SpotifyUrl
                }),
                UserTopTracks = userTopTracks.Select(track => new ListItem
                {
                    MainText = track.Name,
                    SecondText = track.Artist,
                    Image = track.ImageUrl,  
                    SpotifyUrl = track.SpotifyUrl
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
