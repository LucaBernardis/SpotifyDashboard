using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpotifyDashboard.Server.Authentication
{
    public class SpotifyAuth
    {
        private const string ClientId = "480eb2a6091f4a95892f638ade6228e5";
        private const string ClientSecret = "cd525cecdc4142f199756da2fe49ec9f";

        public async Task<string> GetAccessToken()
        {
            using (var client = new HttpClient())
            {
                var authString = $"{ClientId}:{ClientSecret}";
                var base64AuthString = Convert.ToBase64String(Encoding.UTF8.GetBytes(authString));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64AuthString);

                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

                var response = await client.PostAsync("https://accounts.spotify.com/api/token", content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<TokenAuthentication>(responseBody);
                return tokenResponse.AccessToken;
            }
        }
    }
}
