
using Microsoft.AspNetCore.Mvc;
using SpotifyDashboard.Server.Models;
using SpotifyDashboard.Server.Services;

using System.Collections;

namespace SpotifyDashboard.Server.Endpoints
{
    public static class TrackEndPoint
    {

        public static IEndpointRouteBuilder MapTrackEndPoint(this IEndpointRouteBuilder builder)
        {

            // EndPoint Address to call from the front-end
            var group = builder.MapGroup("/serverApi/track")
                .WithTags("tracks");

            // Retrieve the user top tracks
            group.MapGet("/topTenTracks", GetTopTenSongsAsync);

            // Retrieve some recommended tracks based on the passed parameters
            group.MapGet("/getRecommended", GetRecommendedTracks);

            return builder;
        }

        /// <summary>
        /// EndPoint to call the GetTopSons method
        /// </summary>
        /// <param name="token"> The access_token value </param>
        /// <param name="data"> The TrackService instance </param>
        /// <returns> A list of the user's favourite tracks </returns>
        private static async Task<IEnumerable<Track>> GetTopTenSongsAsync([FromHeader(Name = "Authorization")] string token, TrackService data)
        {
            var tracks = await data.GetTopTenSongs(token);
            return tracks;
        }

        /// <summary>
        /// EndPoint to call the GetRecommandedSongs method 
        /// </summary>
        /// <param name="token"> The access_token value </param>
        /// <param name="seedArtist"> The value of the artis id passed in the query params </param>
        /// <param name="seedGenres"> The value of the artis main genre passed in the query params </param>
        /// <param name="seedTracks"> The value of the artist most famous track passed in the query params </param>
        /// <param name="data"> The TrackService instance </param>
        /// <returns> A list of recommended songs based on the query params </returns>
        private static async Task<IEnumerable<Track>> GetRecommendedTracks(
            [FromHeader(Name = "Authorization")] string token,
            [FromQuery] string seedArtist,
            [FromQuery] string seedGenres,
            [FromQuery] string seedTracks,
            TrackService data
            )
        {
            var recommendedTracks = await data.GetRecommendedSongs(token, seedArtist, seedGenres, seedTracks);
            return recommendedTracks;
        }

    }
}
