
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
            // EndPoint Address to call from the front-end
            var group = builder.MapGroup("/serverApi/me")
                .WithTags("User");

            // Retrieve the data about the current user
            group.MapGet("/getData", GetUserDataAsync);

            // Retrive the current user playlists
            group.MapGet("/getPlaylists", GetUserPlaylistsAsync);

            return builder;
        }

        /// <summary>
        /// EndPoint to call the GetUserPlaylist method
        /// </summary>
        /// <param name="token"> The access_token value </param>
        /// <param name="data"> The UserService instance </param>
        /// <returns> A list of thr user's playlists </returns>
        private static async Task<IEnumerable<Playlist>> GetUserPlaylistsAsync([FromHeader(Name = "Authorization")] string token, UserService data)
        {
            var playlists = await data.GetUserPlaylist(token);
            return playlists;
        }

        /// <summary>
        /// EndPoint to call the GetUserData method
        /// </summary>
        /// <param name="token"> The access_token value </param>
        /// <param name="data"> The UserService instance </param>
        /// <returns> An object with the current authenticated user data </returns>
        private static async Task<User> GetUserDataAsync([FromHeader(Name = "Authorization")] string token ,UserService data)
        {   
            var user = await data.GetUserData(token);
            return user;
        }
    }
}
