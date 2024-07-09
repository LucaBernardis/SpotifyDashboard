using MongoDB.Bson;
using MongoDB.Driver;
using SpotifyDashboard.Server.Models.Dashboard;

namespace SpotifyDashboard.Server.Services
{
    /// <summary>
    /// Service to manage the configuration data saved on mongodb
    /// </summary>
    public class ConfigService
    {
        private readonly IMongoClient _client;

        public ConfigService(IMongoClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Method to retrieve all the data about the dashboard configuration saved on mongodb
        /// </summary>
        /// <returns> A <see cref="List{T}"/> of <see cref="WidgetComponent"/> containing all the properties of the widget elements of the collection </returns>
        public async Task<List<WidgetComponent>> GetDashboardConfig()
        {
            var db = _client.GetDatabase("Spotify");
            var collection = db.GetCollection<WidgetComponent>("Tiles");

            var task = await collection.FindAsync<WidgetComponent>(new BsonDocument()); // The Find query to mongodb

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
