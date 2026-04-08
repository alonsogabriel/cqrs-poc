using CqrsPoc.App;
using CqrsPoc.App.Handlers.Books;
using CqrsPoc.Domain.Services;
using CqrsPoc.Infra;
using CqrsPoc.WriteApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<BookService>();
builder.Services.RegisterInfraDependencies();

builder.Services.AddScoped<IMediator, Mediator>();
builder.Services.RegisterHandler<BookHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


var api = app.MapGroup("v1");

api.MapBooksApi();

app.Run();