
namespace SpotifyDashboard.Server.Endpoints
{
    public static class TrackEndPoint
    {

        public static IEndpointRouteBuilder MaptrackendPoint(this IEndpointRouteBuilder builder)
        {

            var group = builder.MapGroup("/track")
                .WithTags("tracks");

            group.MapGet("/dailyTrack", GetDailytrackAsync);

            group.MapGet("/topTenTracks", GetTopTenSongsAsync);

            group.MapGet("/weeklytrack", GetWeeklyTrackAsync);

            group.MapGet("/dailyTime", GetDailyListenTimeAsync);

            group.MapGet("/weeklyTime", GetWeeklyListenTimeAsync);

            return builder;
        }

        private static async Task GetWeeklyListenTimeAsync(HttpContext context)
        {
            throw new NotImplementedException();
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
