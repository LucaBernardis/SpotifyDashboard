
using Microsoft.AspNetCore.Mvc;
using SpotifyDashboard.Server.Services;

namespace SpotifyDashboard.Server.Endpoints
{
    public static class DashboardEndPoint
    {

        public static IEndpointRouteBuilder MapDashboardEndPoint(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("/serverApi/dashboard")
                .WithTags("Retrieve dashboard data");

            group.MapGet("/data", GetDashboardDataAsync);

            return builder;
        }

        private static async Task<object> GetDashboardDataAsync([FromHeader(Name = "Authorization")] string token, DashboardService data)
        {
            var dashboard = await data.GetDashboardData(token);
            return dashboard;
        }
    }
}
