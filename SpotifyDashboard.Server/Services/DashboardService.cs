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

            var data = await GetTopArtist();
            var data2 = await GetArtistTopTrack(data.Id);

            return new
            {
                TopArtist = new
                {
                    Data = data.Name,
                    ImageUrl = data.ImageUrl
                },
                TopTrack = new
                {
                    Data = data2.Name,
                    ImageUrl = data2.ImageUrl
                }
            };
        }
    }
}
