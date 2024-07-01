using Moq;
using Moq.Protected;
using SpotifyDashboard.Server.Models;
using SpotifyDashboard.Server.Services;
using System.Text.Json;
using System.Net;

namespace SpotifyDashboard.Test;

public class SpotifyTests
{

    [Fact(DisplayName = "Ritorna i dati dell'utente")]
    public async void GetTestUserData()
    {
        var mockHandler = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(mockHandler.Object);
        var userService = new UserService();

        var expectedUser = new User("BernardisLuca", "bernardisluca0@gmail.com", "5cpv82m9ahpcgwaru1e2w8fyi", "https://i.scdn.co/image/ab67757000003b8231d09a1c138277ee99cdfb12");

        var responseContent = new StringContent(JsonSerializer.Serialize(expectedUser));
        responseContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = responseContent
            });

        var token = "Bearer BQDiSMzOu3IOWAJmpMNl9NsvM8yTsXk6-SB5drm8IY8BaFESvtkx4WP6i7U01dpoDAh9OtzHMSHsAzqXYYA0pAN7DlCUcYhNkQFClTsKBo1_9H_X6A7cOlMvMS4F_KyxanOmqorTrCB_gG1Q5HV1r_QwaqmzXQgZAHs9b3Qv-MyA6HLFK2TNHWt0qwSG5qCLnFETp34czHjHtS6IkqCg4wIB6zL7OOZxAT91vtCj";

        var result = await userService.GetUserData(token);

        Assert.NotNull(result);
        Assert.Equal(expectedUser, result);
    }

    [Fact(DisplayName = "Ritorna l'artista preferito dall'utente")]
    public void ArtistaPreferito()
    {
        
    }

    [Fact(DisplayName = "Ritorna le 10 canzoni preferite dell'utente (le più ascoltate)")]
    public void Top10Canzoni()
    {

    }
    
}