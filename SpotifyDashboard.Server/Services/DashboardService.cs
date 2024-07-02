using System.Net.Http.Headers;

namespace SpotifyDashboard.Server.Services
{
    // Passo 2
    public partial class DashboardService
    {
        private readonly HttpClient _httpClient;

        public DashboardService(HttpClient httpClient = null)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.spotify.com/");
        }

        public async Task<object> GetDashboardData(string token)
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
            var topTracks = await GetTopTenSongs(); // IEnumerable
            var recommended = await GetRecommendedSongs(topArtist.Id, topArtist.Genres, topArtistTopTrack.Id); // IEnumerable

            // Return object that group the returns of all the methods
            return new
            {   
                // Artist Objects
                TopArtist = new
                {
                    Data = topArtist.Name,
                    Image = topArtist.ImageUrl
                },
                TopTrack = new
                {
                    Data = topArtistTopTrack.Name,
                    Image = topArtistTopTrack.ImageUrl,
                    ArtistName = topArtistTopTrack.Artist
                },
                ArtistAlbums = new
                {
                    
                },
                NewReleases = new
                {

                },
                User = new
                {
                    Name = user.DisplayName,
                    Image = user.Imageurl,
                    UserId = user.Id,
                },
                UserPlaylist = new
                {

                },
                TopTracks = new
                {

                },
                Recommended = new
                {

                }
            };
        }
    }
}
