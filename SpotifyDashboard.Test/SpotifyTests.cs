using Moq;
using Moq.Protected;
using SpotifyDashboard.Server.Models;
using SpotifyDashboard.Server.Services;
using System.Text.Json;
using System.Net;
using System.Text.Json.Nodes;
using SpotifyDashboard.Server.Models.Dashboard;
using MongoDB.Driver;

namespace SpotifyDashboard.Test;

// Passo 1
public class SpotifyTests
{

    // Test per chiamte api a spotify
    
    [Theory(DisplayName = "Ritorna i dati dell'utente corrente")]
    [InlineData("Bernardisluca")]
    public async Task GetTestUserData(string username)
    {
        var mockHandler = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(mockHandler.Object);

        var expectedUser = new User(username, "aaa@gmail.com", "bbb", "https://ccc");
        var image = new[]
        {
        new { url = "https://example.com/image.jpg" }
    };

        var responseContent = new StringContent(JsonSerializer.Serialize(new
        {
            display_name = username,
            email = "aaa@gmail.com",
            images = image
        }));
        responseContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync"
                , ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.ToString().Contains("v1/me"))
                , ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = responseContent,
            });

        var userService = new DashboardService(httpClient);
        var result = await userService.GetUserData();

        Assert.NotNull(result);
        Assert.Equal(expectedUser.DisplayName, result.DisplayName);
    }


    [Theory(DisplayName = "Ritorna l'artista preferito dall'utente")]
    [InlineData("Ikka")]
    public async Task TestArtistaPreferito(string favouriteArtist)
    {
        // Setup
        var expectedArtist = new Artist("bbb", "aaa", "https://aaaaaaa", favouriteArtist);

        var mockResponse = new JsonObject
        {
            ["items"] = new JsonArray
        {
            new JsonObject
            {
                ["name"] = favouriteArtist,
                ["genres"] = new JsonArray { "Prog rock", "Grunge" },
                ["images"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["url"] = "https://aaaaaaa"
                    }
                }
            }
        }
        };

        var mockHandler = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(mockHandler.Object);

        mockHandler.Protected()
           .Setup<Task<HttpResponseMessage>>(
                "SendAsync"
               , ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.ToString().Contains("v1/me/top/artists?limit=1"))
               , ItExpr.IsAny<CancellationToken>())
           .ReturnsAsync(new HttpResponseMessage
           {
               StatusCode = HttpStatusCode.OK,
               Content = new StringContent(mockResponse.ToJsonString()),
           });

        // Act
        var artistService = new DashboardService(httpClient);
        var result = await artistService.GetTopArtist();

        // Verify
        Assert.NotNull(result);
        Assert.Equal(expectedArtist.Name, result.Name);
    }


    [Theory(DisplayName = "Ritorna la miglior canzone dell' artista preferito dell'utente")]
    [InlineData("NomeTraccia", "aaabbbccc")]
    public async Task MigliorTracciaArtistaPreferito( string trackName, string id)
    {
        var mockHandler = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(mockHandler.Object);

        var expectedTrack = new Track("aaa", "bbb", "https://ccc", trackName);

        var mockResponse = new JsonObject
        {
            ["tracks"] = new JsonArray
        {
            new JsonObject
            {
                ["name"] = trackName,
                ["album"] = new JsonObject
                {
                    ["images"] = new JsonArray
                    {
                        new JsonObject
                        {
                            ["url"] = "https://i.scdn.co/image/ab67616d00001e02ff9ca10b55ce82ae553c8228"
                        }
                    }
                },
                ["artists"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["name"] = trackName
                    }
                }
            }
        }
        };

        var responseContent = new StringContent(mockResponse.ToJsonString());
        responseContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync"
                , ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.ToString().Contains($"/top-tracks"))
                , ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = responseContent,
            });

        var artistService = new DashboardService(httpClient);
        var result = await artistService.GetArtistTopTrack(id);

        Assert.NotNull(result);
        Assert.Equal(expectedTrack.Name, result.Name);
    }


    // Test per mongodb
    [Theory(DisplayName = "Ritorna la risponsa con dati mock per il client mongo")]
    [InlineData("WidgetComponent1")]
    public async Task Test_GetDashboardConfig(string widgetComponentName)
    {
        // Arrange
        var mockCollection = new Mock<IMongoCollection<WidgetComponent>>();
        var mockCursor = new Mock<IAsyncCursor<WidgetComponent>>();
        var mockClient = new Mock<IMongoClient>();
        var mockDatabase = new Mock<IMongoDatabase>();

        // Create some test data (replace with your actual data)
        var testData = new List<WidgetComponent>
        {
            new WidgetComponent (widgetComponentName),
        };

        // Set up the mock cursor
        int callCount = 0;
        mockCursor.Setup(_ => _.Current).Returns(testData);
        mockCursor.Setup(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                   .ReturnsAsync(() => callCount++ < testData.Count);

        // Set up the mock collection
        mockCollection.Setup(c => c.FindAsync(It.IsAny<FilterDefinition<WidgetComponent>>(), It.IsAny<FindOptions<WidgetComponent, WidgetComponent>>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(mockCursor.Object);

        // Set up the mock database
        mockDatabase.Setup(d => d.GetCollection<WidgetComponent>("Tiles", null)).Returns(mockCollection.Object);

        // Set up the mock client
        mockClient.Setup(c => c.GetDatabase("Spotify", null)).Returns(mockDatabase.Object);

        // Create an instance of the class under test
        var service = new ConfigService(mockClient.Object); // Replace "YourClass" with the actual class name

        // Act
        var result = await service.GetDashboardConfig();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Exists(w => w.WidgetName == widgetComponentName));
    }

    [Fact(DisplayName = "Se DB vuoto allora inserisci i widget standard con le loro proprieta")]
    public async Task InserisciDatiSuSBVuoto()
    {
        // Arrange
        var mockCollection = new Mock<IMongoCollection<WidgetComponent>>();
        var mockCursor = new Mock<IAsyncCursor<WidgetComponent>>();
        var mockClient = new Mock<IMongoClient>();
        var mockDatabase = new Mock<IMongoDatabase>();

        // Create some test data (replace with your actual data)
        var testData = new List<WidgetComponent>
        {
            new WidgetComponent("pippo")
        };

        // Set up the mock cursor
        int callCount = 0;
        mockCursor.Setup(_ => _.Current).Returns(testData);
        mockCursor.Setup(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                   .ReturnsAsync(() => callCount++ < testData.Count);

        // Set up the mock collection
        mockCollection.Setup(c => c.FindAsync(It.IsAny<FilterDefinition<WidgetComponent>>(), It.IsAny<FindOptions<WidgetComponent, WidgetComponent>>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(mockCursor.Object);

        // Set up the mock database
        mockDatabase.Setup(d => d.GetCollection<WidgetComponent>("Tiles", null)).Returns(mockCollection.Object);

        // Set up the mock client
        mockClient.Setup(c => c.GetDatabase("Spotify", null)).Returns(mockDatabase.Object);
        var service = new ConfigService(mockClient.Object);

        var checkDb = await service.GetDashboardConfig();

        if(checkDb.Count == 0)
        {
            await service.CreateDashboardWidgets();
            await service.GetDashboardConfig();
        }

        Assert.NotEqual(checkDb.Count, 0);
    }

}