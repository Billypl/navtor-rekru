using NavtorShiper.Repositories;
using NavtorShiper.Services;
using NavtorShiper.Data;
using System.Text.Json.Serialization.Metadata;
using NavtorShiper.Entities;
using Microsoft.Extensions.DependencyInjection;


Console.WriteLine(ShipType.Tanker.ToString());
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();



builder.Services.AddSingleton<IShipRepository, ShipRepository>();
builder.Services.AddScoped<Seeder>();

builder.Services.AddScoped<IShipService, ShipService>();
builder.Services.AddScoped<IPassengerShipService, PassengerShipService>();
builder.Services.AddScoped<ITankerShipService, TankerShipService>();

var app = builder.Build();
var seeder = app.Services.CreateScope().ServiceProvider.GetRequiredService<Seeder>();
seeder.FillShipRepository();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
