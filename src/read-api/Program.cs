using CqrsPoc.App;
using CqrsPoc.App.Repositories;
using CqrsPoc.Infra;
using CqrsPoc.ReadApi;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.RegisterInfraDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var api = app.MapGroup("v1");

api.MapBooksApi();

using var scope = app.Services.CreateScope();
using var consumer = scope.ServiceProvider.GetRequiredService<IBookConsumer>();
await consumer.Init();

app.Run();
