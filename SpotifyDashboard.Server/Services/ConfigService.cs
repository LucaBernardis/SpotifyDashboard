using MongoDB.Bson;
using MongoDB.Driver;
using SpotifyDashboard.Server.Models.Dashboard;

namespace SpotifyDashboard.Server.Services
{
    /// <summary>
    /// Service to manage the <see cref="WidgetComponent"/> configuration data saved on mongodb
    /// </summary>
    public class ConfigService
    {
        private readonly IMongoClient _client;

        public ConfigService(IMongoClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Method to retrieve all the data about the <see cref="WidgetComponent"/> configuration saved on mongodb
        /// </summary>
        /// <returns> A <see cref="List{T}"/> of <see cref="WidgetComponent"/> </returns>
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

            if (list.Count == 0)
            {
                await CreateDashboardWidgets();
                await GetDashboardConfig();
            }
            return list;
        }

        /// <summary>
        /// If the database is empty, it insert the standard widget with their properties on the mongo db
        /// <seealso cref="GetDashboardConfig"/>
        /// </summary>
        /// <returns></returns>
        public async Task CreateDashboardWidgets()
        {
            // Setting mongo environment
            var db = _client.GetDatabase("Spotify");
            var collection = db.GetCollection<WidgetComponent>("Tiles");

            var userDataWidget = new WidgetComponent
            {
                WidgetName = "user-data",
                WidgetProperty = "user",
                WidgetLabel = "Dati dell'utente corrente",
                Type = "header",
                Heigth = 1,
                Width = 4,
                Position = "center"
            };
            var topArtistWidget = new WidgetComponent
            {
                WidgetName = "top-artist",
                WidgetProperty = "topArtist",
                WidgetLabel = "Favourite",
                Type = "card",
                Heigth = 1,
                Width = 4,
                Position = "left"
            };
            var userPlaylist = new WidgetComponent
            {
                WidgetName = "user-playlist",
                WidgetProperty = "userPlaylists",
                WidgetLabel = "Playlists",
                Type = "list",
                Heigth = 4,
                Width = 4,
                Position = "right"
            };
            var topArtistSongWidget = new WidgetComponent
            {
                WidgetName = "top-artist-song",
                WidgetProperty = "artistTopTrack",
                WidgetLabel = "Artist Top Track",
                Type = "card",
                Heigth = 1,
                Width = 2,
                Position = "center"
            };
            var topSongsWidget = new WidgetComponent
            {
                WidgetName = "top-ten-songs",
                WidgetProperty = "userTopTracks",
                WidgetLabel = "Favourite Tracks",
                Type = "list",
                Heigth = 3,
                Width = 4,
                Position = "left"
            };
            var newReleasesWidget = new WidgetComponent
            {
                WidgetName = "new-releases",
                WidgetProperty = "newReleases",
                WidgetLabel = "New Releases",
                Type = "list",
                Heigth = 1,
                Width = 2,
                Position = "center"
            };
            var topGenresWidget = new WidgetComponent
            {
                WidgetName = "top-genres",
                WidgetProperty = "topGenres",
                WidgetLabel = "Recommended tracks & Artist Albums",
                Type = "multi-list",
                Heigth = 2,
                Width = 4,
                Position = "center"
            };

            var widgets = new[]
            {
                userDataWidget,
                topArtistWidget,
                userPlaylist,
                topArtistSongWidget,
                topSongsWidget,
                newReleasesWidget,
                topGenresWidget
            };

            await collection.InsertManyAsync(widgets);

        }
    }
}
