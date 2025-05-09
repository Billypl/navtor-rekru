using NavtorShiper.Repositories;
using NavtorShiper.Services;
using NavtorShiper.Data;
using NavtorShiper.Entities;
using NavtorShiper.Web.Middlewares;


Console.WriteLine(ShipType.Tanker.ToString());
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ErrorHandlerMiddleware>();

builder.Services.AddSingleton<IShipRepository, ShipRepository>();
builder.Services.AddScoped<Seeder>();

builder.Services.AddScoped<IShipService, ShipService>();
builder.Services.AddScoped<IPassengerShipService, PassengerShipService>();
builder.Services.AddScoped<ITankerShipService, TankerShipService>();

var app = builder.Build();
var seeder = app.Services.CreateScope().ServiceProvider.GetRequiredService<Seeder>();
seeder.FillShipRepository();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Ship API v1");
    c.RoutePrefix = "docs";
});
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
