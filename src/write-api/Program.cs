using CqrsPoc.WriteApi;
using CqrsPoc.WriteApi.App;
using CqrsPoc.WriteApi.Domain.Repositories;
using CqrsPoc.WriteApi.Domain.Services;
using CqrsPoc.WriteApi.Infra;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>((sp, options) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connstr = config.GetConnectionString("Postgres");
    options.UseNpgsql(connstr);
});

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookCategoryRepository, BookCategoryRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookProducer, BookProducer>();

builder.Services.AddScoped<BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


var api = app.MapGroup("v1");

api.MapBooksApi();

app.Run();