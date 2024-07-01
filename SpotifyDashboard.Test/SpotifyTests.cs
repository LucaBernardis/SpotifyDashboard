using Moq;
using Moq.Protected;
using SpotifyDashboard.Server.Models;
using SpotifyDashboard.Server.Services;
using System.Text.Json;
using System.Net;

namespace SpotifyDashboard.Test;

// Passo 1
public class SpotifyTests
{

    // TODO: Rifare test usando Theory e InlineData
    [Fact(DisplayName = "Ritorna i dati dell'utente corrente")]
    public async void GetTestUserData()
    {
        var mockHandler = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(mockHandler.Object);
        var userService = new UserService();

        var expectedUser = new User("Bernardisluca", "bernardisluca0@gmail.com", "5cpv82m9ahpcgwaru1e2w8fyi", "https://i.scdn.co/image/ab67757000003b8231d09a1c138277ee99cdfb12");

        var responseContent = new StringContent(JsonSerializer.Serialize(expectedUser));
        responseContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = responseContent
            });

        var token = "Bearer BQC2Z6RUlxrxPxt5PasWFQhI9jJiLJZwyCey6RDen1ytuqrA-3C1BB241aSBYWAlZ6wDv2PEb0lGFXkiEONrhLpzXHSfQatsLCBLjuKucqZuRU_7zWClpTBXJjThW0xiVFdwvY4d2u9_y5A-LgrlNGi6Vl5uAV1Hjbtx-FtZo-dcLgnX4RoVvdHFXGR-1wteJ03FQJ5xCTA3QRRbxu9XVUbAxqsj724E5P7LOA0W";

        var result = await userService.GetUserData(token);

        Assert.NotNull(result);
        Assert.Equal(expectedUser.ToString(), result.ToString());
    }

    [Theory(DisplayName = "Ritorna l'artista preferito dall'utente")]
    [InlineData(
        "Bearer BQC2Z6RUlxrxPxt5PasWFQhI9jJiLJZwyCey6RDen1ytuqrA-3C1BB241aSBYWAlZ6wDv2PEb0lGFXkiEONrhLpzXHSfQatsLCBLjuKucqZuRU_7zWClpTBXJjThW0xiVFdwvY4d2u9_y5A-LgrlNGi6Vl5uAV1Hjbtx-FtZo-dcLgnX4RoVvdHFXGR-1wteJ03FQJ5xCTA3QRRbxu9XVUbAxqsj724E5P7LOA0W",
        "Ikka"
    )]
    public async void TestArtistaPreferito(string token, string favouriteArtist)
    {
        // Setup
        var expectedArtist = new Artist("aaaa", "aaaaa", "https://blblabla", favouriteArtist);

        var mockHandler = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(mockHandler.Object);

        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync"
                , ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.ToString().Contains("v1/me/top/artists?limit=1"))
                //&& req.Headers.Contains("Authorization"))
                , ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(expectedArtist)),                
            });

        // Act
        var artistService = new DashboardService(httpClient);
        var result = await artistService.GetTopArtist();

        // Verify
        Assert.NotNull(result);
        Assert.Equal(expectedArtist.Name, result.Name);
    }

    // TODO: Rifare test usando Theory e InlineData
    [Fact(DisplayName = "Ritorna la miglior canzone dell' artista preferito dell'utente")]
    public async void MigliorTracciaArtistaPreferito()
    {
        var mockHandler = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(mockHandler.Object);
        var artistService = new DashboardService();

        var expectedTrack = new Track("Ikka", 225099, "5Zv9GfbJv0MVntvTGF0IwG", "https://i.scdn.co/image/ab67616d0000b2732815e8a9065df815fe584baa", "Jagga Jatt");

        var responseContent = new StringContent(JsonSerializer.Serialize(expectedTrack));
        responseContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = responseContent
            });

        var token = "Bearer BQC2Z6RUlxrxPxt5PasWFQhI9jJiLJZwyCey6RDen1ytuqrA-3C1BB241aSBYWAlZ6wDv2PEb0lGFXkiEONrhLpzXHSfQatsLCBLjuKucqZuRU_7zWClpTBXJjThW0xiVFdwvY4d2u9_y5A-LgrlNGi6Vl5uAV1Hjbtx-FtZo-dcLgnX4RoVvdHFXGR-1wteJ03FQJ5xCTA3QRRbxu9XVUbAxqsj724E5P7LOA0W";
        var id = "07iEy1AecUPVzfC2J2gCHR";

        var result = await artistService.GetAlbums(token, id);

        Assert.NotNull(result);
        Assert.Equal(expectedTrack.ToString(), result.ToString());
    }
}