
using Microsoft.AspNetCore.Mvc;
using SpotifyDashboard.Server.Models;
using SpotifyDashboard.Server.Services;

namespace SpotifyDashboard.Server.Endpoints
{
    public static class ArtistEndPoint
    {

        public static IEndpointRouteBuilder MapArtistEndPoint(this IEndpointRouteBuilder builder)
        {

            var group = builder.MapGroup("/serverApi/artist")
                .WithTags("Artist");

            group.MapGet("/data", GetArtistDataAsync);

            group.MapGet("/topArtist", GetUserTopArtist);

            return builder;
        }

        private static async Task GetArtistDataAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static async Task<Artist> GetUserTopArtist([FromHeader(Name = "Authorization")] string token, ArtistService data)
        {
            var topArtist = await data.GetTopArtist(token);
            return topArtist;
        }
    }
}
