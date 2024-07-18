using MongoDB.Driver;
using SpotifyDashboard.Server.Endpoints;
using SpotifyDashboard.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<ConfigService>();
builder.Services.AddCors();
builder.Services.AddHttpClient();

var configuration = builder.Configuration;

var connectionString = configuration.GetConnectionString("MongoString");

builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(connectionString)); // Add connection string to the mongodb

var app = builder.Build();

app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
    app.UseCors(app =>
    {
        app.AllowAnyOrigin();
        app.AllowAnyMethod();
        app.AllowAnyHeader();
    });

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
if (app.Environment.IsProduction())
    app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

// Mapping created Enpoints
app.MapDashboardEndPoint(); // This Endpoint manage all the methods calls to make the widget on the dashboard work

var configService = app.Services.GetRequiredService<ConfigService>();
var dbContent = await configService.GetDashboardConfig();
if (dbContent.Count == 0)
    await configService.CreateDashboardWidgets();

await app.RunAsync();
