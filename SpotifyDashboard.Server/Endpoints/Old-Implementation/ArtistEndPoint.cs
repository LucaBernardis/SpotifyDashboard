
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

            group.MapGet("/newReleases", GetNewReleases);

            return builder;
        }

        /// <summary>
        /// Endpoint to call the GetNewReleases method
        /// </summary>
        /// <param name="token"> The access_token value </param>
        /// <param name="data"> The ArtistService instance </param>
        /// <returns></returns>
        private static async Task<IEnumerable<Album>> GetNewReleases([FromHeader(Name = "Authorization")] string token, DashboardService data)
        {
            var newReleases = await data.GetNewReleases();
            return newReleases;
        }

        /// <summary>
        /// EndPoint to call the GetArtistAlbums method
        /// </summary>
        /// <param name="token"> The access_token value </param>
        /// <param name="id"> The value of the artist id </param>
        /// <param name="data"> The ArtistService instance</param>
        /// <returns> A list of albums </returns>
        private static async Task<IEnumerable<Album>> GetArtistAlbums([FromHeader(Name = "Authorization")] string token, [FromRoute] string id, DashboardService data)
        {
            var albums = await data.GetArtistAlbums(id);
            return albums;
        }

        /// <summary>
        /// EndPoint to call the GetArtistTopTrack method
        /// </summary>
        /// <param name="token"> The access_token value </param>
        /// <param name="id"> The value of the artist id </param>
        /// <param name="data"> The ArtistService instance</param>
        /// <returns> The artist top track </returns>
        private static async Task<Track> GetTopArtistTopSong([FromHeader(Name = "Authorization")] string token, [FromRoute] string id, DashboardService data)
        {
            var topTrack = await data.GetArtistTopTrack(id);
            return topTrack;
        }

        /// <summary>
        /// Enpoint to call the GetTopArtist method
        /// </summary>
        /// <param name="token"> The access_token value </param>
        /// <param name="data"> The ArtistService instance</param>
        /// <returns> The user's favourite artist </returns>
        private static async Task<Artist> GetUserTopArtist([FromHeader(Name = "Authorization")] string token, DashboardService data)
        {
            var topArtist = await data.GetTopArtist();
            return topArtist;
        }
    }
}
