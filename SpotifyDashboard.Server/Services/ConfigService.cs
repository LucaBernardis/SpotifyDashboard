using MongoDB.Driver;

namespace SpotifyDashboard.Server.Services
{
    // Passo 3
    public class ConfigService
    {
        private readonly IMongoClient _client;

        public ConfigService(IMongoClient client)
        {
            _client = client;
        }


        public async Task GetDashboardConfig()
        {

            throw new NotImplementedException();
            //var list = _client.GetDatabase("Spotify")
            //    .GetCollection("Tiles")
            //    .Find();

            //return list;
        }
    }
}
