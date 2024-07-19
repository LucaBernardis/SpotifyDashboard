using Microsoft.AspNetCore.Mvc;
using SpotifyDashboard.Server.Models.Dashboard;
using SpotifyDashboard.Server.Services;

namespace SpotifyDashboard.Server.Endpoints
{
    public static class DashboardEndPoint
    {
        /// <summary>
        /// EndPoint Mapper for the api calls to retrieve the usefull data about the dashboard and the configuration
        /// </summary>
        /// <param name="builder">The builder to specify the routes for the application</param>
        /// <returns>A <see cref="IEndpointRouteBuilder"/> object</returns>
        public static IEndpointRouteBuilder MapDashboardEndPoint(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("/serverApi/dashboard")
                .WithTags("Dashboard")
                .WithDescription("Retrieve Dashboard data");

            group.MapGet("/data", GetDashboardDataAsync)
                .WithName("Get Dashboard Data")
                .WithDescription("Retrieve usefull data to pupolate the dashboard widgets");

            group.MapGet("/config", GetDashboardConfigAsync)
                .WithName("Get Dashboard Configuration")
                .WithDescription("Retrieve the configuration data about the dashboard widgets");

            return builder;
        }


        /// <summary>
        /// Endpoint method to call <see cref="DashboardService.GetDashboardData(string)"/> and retrieve the data about the dashboard
        /// </summary>
        /// <param name="token"> The access_token value you need to pass in any api call to be authorized </param>
        /// <param name="data"> A <see cref="DashboardService"/> instance </param>
        /// <returns> A <see cref="Dashboard"/> object </returns>
        private static async Task<Dashboard> GetDashboardDataAsync([FromHeader(Name = "Authorization")] string token, DashboardService data)
        {
            var dashboard = await data.GetDashboardData(token);
            return dashboard;
        }


        /// <summary>
        /// Enpoint method to call <see cref="ConfigService.GetDashboardConfig"/> to retrieve the configuration data 
        /// </summary>
        /// <param name="data"> A <see cref="ConfigService"/> instance </param>
        /// <returns> A <see cref="List{T}"/> of <see cref="WidgetComponent"/> </returns>
        private static async Task<IEnumerable<WidgetComponent>> GetDashboardConfigAsync(ConfigService data)
        {
            var config = await data.GetDashboardConfig();
            return config;
        }

        
    }
}
