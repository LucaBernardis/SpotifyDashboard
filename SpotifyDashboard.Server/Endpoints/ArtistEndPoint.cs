
namespace SpotifyDashboard.Server.Endpoints
{
    public static class ArtistEndPoint
    {

        public static IEndpointRouteBuilder MapArtistEndPoint(this IEndpointRouteBuilder builder)
        {

            var group = builder.MapGroup("/artist")
                .WithTags("Artist");

            group.MapGet("/data", GetArtistDataAsync);

            group.MapGet("/topArtist", GetUserTopArtist);

            return builder;
        }

        private static async Task GetUserTopArtist(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static async Task GetArtistDataAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
