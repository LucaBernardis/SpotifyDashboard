
using SpotifyDashboard.Server.Models;

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

        private static async Task GetTopTenSongsAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static async Task GetDailytrackAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
