using Moq;
using Moq.Protected;
using SpotifyDashboard.Server.Models;
using SpotifyDashboard.Server.Services;
using System.Text.Json;
using System.Net;
using System.Text.Json.Nodes;

namespace SpotifyDashboard.Test;

// Passo 1
public class SpotifyTests
{

    [Theory(DisplayName = "Ritorna i dati dell'utente corrente")]
    [InlineData("Bearer aaa","Bernardisluca")]
    public async void GetTestUserData(string token, string username)
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
    [InlineData("Bearer aaa","Ikka")]
    public async void TestArtistaPreferito(string token, string favouriteArtist)
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

    // TODO: Rifare test usando Theory e InlineData
    [Theory(DisplayName = "Ritorna la miglior canzone dell' artista preferito dell'utente")]
    [InlineData("Bearer aaa", "NomeTraccia", "aaabbbccc")]

    public async void MigliorTracciaArtistaPreferito(string token, string trackName, string id)
    {
        var mockHandler = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(mockHandler.Object);

        var expectedTrack = new Track("aaa", 000, "bbb", "https://ccc", trackName);

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
}