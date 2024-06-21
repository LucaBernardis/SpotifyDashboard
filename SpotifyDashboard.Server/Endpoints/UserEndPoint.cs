
using Microsoft.AspNetCore.Http.HttpResults;
using SpotifyDashboard.Server.Models;
using SpotifyDashboard.Server.Services;

namespace SpotifyDashboard.Server.Endpoints
{
    public static class UserEndPoint
    {
        public static IEndpointRouteBuilder MapUserEndPoint(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("/me")
                .WithTags("User");

            group.MapGet("/getData", GetUserDataAsync);


            return builder;
        }

        private static async Task<User> GetUserDataAsync(UserService data)
        {
            var user = await data.GetUserData();
            return user;
        }
    }
}
