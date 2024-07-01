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
builder.Services.AddScoped<TrackService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddCors();
builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(
    
    ));

var app = builder.Build();

app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
    app.UseCors(app =>
    {
        app.AllowAnyOrigin();
        app.AllowAnyMethod();
        app.AllowAnyHeader();
    });


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Mapping created Enpoints
app.MapUserEndPoint();
app.MapTrackEndPoint();
app.MapArtistEndPoint();

// Passo 5
//var mongoclient = app.Services.GetService<IMongoClient>();
//var tiles = mongoclient.GetDatabase("S").GetCollection("T");
//if(tiles.Count() == 0)
//{
//    // insert
//}


app.Run();
