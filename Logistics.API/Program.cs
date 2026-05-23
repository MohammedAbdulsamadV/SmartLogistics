using Logistics.Application.Common;
using Logistics.Infrastucture.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSignalR();
builder.Services.AddScoped<ITrackingHubService, TrackingHubService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseMiddleware<Logistics.API.Middlewares.ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();




app.Run();

