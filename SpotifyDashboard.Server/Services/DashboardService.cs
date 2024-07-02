using System.Net.Http.Headers;

namespace SpotifyDashboard.Server.Services
{
    // Passo 2
    public partial class DashboardService
    {
        private readonly HttpClient _httpClient;

        public DashboardService()
        {
            _httpClient = new HttpClient();
        }

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
            var topTracks = await GetTopTenSongs(); // IEnumerable
            var recommended = await GetRecommendedSongs(topArtist.Id, topArtist.Genres, topArtistTopTrack.Id); // IEnumerable

            // Return object that group the returns of all the methods
            return new
            {   
                // Artist Objects
                TopArtist = new
                {
                    Name = topArtist.Name,
                    Image = topArtist.ImageUrl
                },
                ArtistTopTrack = new
                {
                    Name = topArtistTopTrack.Name,
                    Image = topArtistTopTrack.ImageUrl,
                    ArtistName = topArtistTopTrack.Artist
                },
                ArtistAlbums = new
                {
                    // Gestisci IEnumerable
                },
                NewReleases = new
                {
                    // Gestisci IEnumerable
                },
                User = new
                {
                    Name = user.DisplayName,
                    Image = user.Imageurl,
                    Id = user.Id,
                },
                UserPlaylist = new
                {
                    // Gestisci IEnumerable
                },
                UserTopTracks = new
                {
                    // Gestisci IEnumerable
                },
                Recommended = new
                {
                    // Gestisci IEnumerable
                }
            };
        }
    }
}
