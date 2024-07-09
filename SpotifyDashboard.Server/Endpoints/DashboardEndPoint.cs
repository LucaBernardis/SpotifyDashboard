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
                .WithDescription("Retrieve Dashboard data");

            group.MapGet("/data", GetDashboardDataAsync)
                .WithDescription("Retrieve usefull data to pupolate the dashboard widgets");

            group.MapGet("/config", GetDashboardConfigAsync)
                .WithDescription("Retrieve the configuration data about the dashboard widgets");

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
