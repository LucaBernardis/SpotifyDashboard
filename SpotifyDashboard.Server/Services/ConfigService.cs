using MongoDB.Driver;

namespace SpotifyDashboard.Server.Services
{
    // Passo 3
    //public class ConfigService
    //{
    //    private readonly IMongoClient _client;

    //    public ConfigService(IMongoClient client)
    //    {
    //        _client = client;
    //    }

    //    public WidgetConfig GetDashboardConfig()
    //    {
    //        var list = _client.GetDatabase("Spotify")
    //            .GetCollection<WidgetConfig>("Tiles")
    //            .Find<WidgetConfig>();

    //        return list;
    //    }
    //}
}
