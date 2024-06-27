
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SpotifyDashboard.Server.Models;
using SpotifyDashboard.Server.Services;

namespace SpotifyDashboard.Server.Endpoints
{
    public static class UserEndPoint
    {
        public static IEndpointRouteBuilder MapUserEndPoint(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("/serverApi/me")
                .WithTags("User");

            group.MapGet("/getData", GetUserDataAsync);

            group.MapGet("/getPlaylists", GetUserPlaylistsAsync);

            return builder;
        }

        private static async Task<IEnumerable<Playlist>> GetUserPlaylistsAsync([FromHeader(Name = "Authorization")] string token, UserService data)
        {
            var playlists = await data.GetUserPlaylist(token);
            return playlists;
        }

        private static async Task<User> GetUserDataAsync([FromHeader(Name = "Authorization")] string token ,UserService data)
        {   
            var user = await data.GetUserData(token);
            return user;
        }
    }
}
