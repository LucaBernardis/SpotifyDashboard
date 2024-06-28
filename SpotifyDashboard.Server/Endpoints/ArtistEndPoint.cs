
using Microsoft.AspNetCore.Mvc;
using SpotifyDashboard.Server.Models;
using SpotifyDashboard.Server.Services;

namespace SpotifyDashboard.Server.Endpoints
{
    public static class ArtistEndPoint
    {

        public static IEndpointRouteBuilder MapArtistEndPoint(this IEndpointRouteBuilder builder)
        {

            // EndPoint Address to call from the front-end
            var group = builder.MapGroup("/serverApi/artist")
                .WithTags("Artist");

            // Retrieve the data of the urser's favourite artist
            group.MapGet("/topArtist", GetUserTopArtist);

            // Retrieve the best track of the user's favourite artist
            group.MapGet("/topArtistTrack/{id}", GetTopArtistTopSong);

            // Retrieve the albums of the user's favourite artist
            group.MapGet("/getAlbums/{id}", GetArtistAlbums);

            return builder;
        }

        private static async Task<IEnumerable<Album>> GetArtistAlbums([FromHeader(Name = "Authorization")] string token, [FromRoute] string id, ArtistService data)
        {
            var albums = await data.GetAlbums(token, id);
            return albums;
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
