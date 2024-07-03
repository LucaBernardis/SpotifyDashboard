using MongoDB.Bson;
using MongoDB.Driver;
using SpotifyDashboard.Server.Models.Dashboard;

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


        public async Task<List<WidgetComponent>> GetDashboardConfig()
        {
            var db = _client.GetDatabase("Spotify");
            var collection = db.GetCollection<WidgetComponent>("Tiles");
            //var filter = Builders<WidgetComponent>.Filter.Eq("name", "user-data");
            var task = await collection.FindAsync(new BsonDocument());

            var list = new List<WidgetComponent>();

            while (await task.MoveNextAsync())
            {
                foreach (var document in task.Current)
                {
                    list.Add(document);
                }
            }

            return list;
        }
    }
}
