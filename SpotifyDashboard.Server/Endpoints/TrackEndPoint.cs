
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

            var group = builder.MapGroup("/serverApi/track")
                .WithTags("tracks");

            group.MapGet("/topTenTracks", GetTopTenSongsAsync);


            return builder;
        }


        private static async Task<IEnumerable<Track>> GetTopTenSongsAsync([FromHeader(Name = "Authorization")] string token, TrackService data)
       {
            var tracks = await data.GetTopTenSongs(token);
            return tracks;
        }

    }
}
