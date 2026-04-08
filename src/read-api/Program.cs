using CqrsPoc.ReadApi;
using CqrsPoc.ReadApi.App;
using CqrsPoc.ReadApi.App.Repositories;
using CqrsPoc.ReadApi.Infra.Repositories;
using Microsoft.OpenApi;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

ConventionRegistry.Register("SnakeCase", new ConventionPack
{
    new CamelCaseElementNameConvention()
}, t => true);

builder.Services.AddScoped(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connstr = config.GetConnectionString("MongoDB");

    return new MongoClient(connstr);
});

builder.Services.AddScoped(sp =>
{
    return sp.GetRequiredService<MongoClient>()
        .GetDatabase("library");
});

builder.Services.AddScoped<IBookReadRepository, BookReadRepository>();

builder.Services.AddScoped<IBookConsumer, BookConsumer>();

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
