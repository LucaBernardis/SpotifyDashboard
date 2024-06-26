
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

            group.MapGet("/dailyTrack", GetDailytrackAsync);

            group.MapGet("/topTenTracks", GetTopTenSongsAsync);

            group.MapGet("/weeklytrack", GetWeeklyTrackAsync);

            group.MapGet("/dailyTime", GetDailyListenTimeAsync);

            group.MapGet("/weeklyTime", GetWeeklyListenTimeAsync);

            return builder;
        }

        private static async Task<Track> GetWeeklyListenTimeAsync(HttpContext context)
        {
            return new Track();
        }

        private static async Task GetDailyListenTimeAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static async Task GetWeeklyTrackAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static async Task<IEnumerable<Track>> GetTopTenSongsAsync([FromHeader(Name = "Authorization")] string token, TrackService data)
       {
            var tracks = await data.GetTopTenSongs(token);
            return tracks;
        }

        private static async Task GetDailytrackAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
