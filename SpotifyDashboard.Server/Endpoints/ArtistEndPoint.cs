
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


            group.MapGet("/topArtist", GetUserTopArtist);

            group.MapGet("/topArtistTrack/{id}", GetTopArtistTopSong);

            return builder;
        }

        private static async Task<Track> GetTopArtistTopSong([FromHeader(Name = "Authorization")] string token, [FromRoute] string id, ArtistService data)
        {
            var topTrack = await data.GetArtistTopTrack(token, id);
            return topTrack;
        }


        private static async Task<Artist> GetUserTopArtist([FromHeader(Name = "Authorization")] string token, ArtistService data)
        {
            var topArtist = await data.GetTopArtist(token);
            return topArtist;
        }
    }
}
