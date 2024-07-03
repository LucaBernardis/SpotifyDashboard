
using Microsoft.AspNetCore.Mvc;
using SpotifyDashboard.Server.Models.Dashboard;
using SpotifyDashboard.Server.Services;

namespace SpotifyDashboard.Server.Endpoints
{
    public static class DashboardEndPoint
    {

        public static IEndpointRouteBuilder MapDashboardEndPoint(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("/serverApi/dashboard")
                .WithTags("Dashboard")
                .WithDescription("Retrieve Dashboard data for the components");

            group.MapGet("/data", GetDashboardDataAsync);

            group.MapGet("/config", GetDashboardConfigAsync);

            return builder;
        }

        private static async Task<IEnumerable<WidgetComponent>> GetDashboardConfigAsync(ConfigService data)
        {
            var config = await data.GetDashboardConfig();
            return config;
        }

        private static async Task<Dashboard> GetDashboardDataAsync([FromHeader(Name = "Authorization")] string token, DashboardService data)
        {
            var dashboard = await data.GetDashboardData(token);
            return dashboard;
        }
    }
}
