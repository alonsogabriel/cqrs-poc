using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CqrsPoc.Domain.Repositories;
using CqrsPoc.WriteApi.App;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using CqrsPoc.App.Repositories;
using CqrsPoc.App;
using CqrsPoc.Domain;

namespace CqrsPoc.Infra;

public static class Utils
{
    public static IServiceCollection RegisterInfraDependencies(this IServiceCollection services)
    {
        ConventionRegistry.Register("SnakeCase", new ConventionPack
        {
            new CamelCaseElementNameConvention()
        }, t => true);

        return services.AddDbContext<AppWriteDatabase>((sp, options) =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var connstr = config.GetConnectionString("Postgres");
            options.UseNpgsql(connstr);
        })
        .AddScoped<IBookRepository, BookRepository>()
        .AddScoped<IBookCategoryRepository, BookCategoryRepository>()
        .AddScoped<IAuthorRepository, AuthorRepository>()
        .AddScoped<IPublisherRepository, PublisherRepository>()
        .AddScoped<IUnitOfWork, UnitOfWork>()
        .AddScoped<IBookProducer, BookProducer>()
        .AddScoped(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var connstr = config.GetConnectionString("MongoDB");

            return new MongoClient(connstr);
        })
        .AddScoped(sp =>
        {
            return sp.GetRequiredService<MongoClient>()
                .GetDatabase("library");
        })
        .AddScoped<IBookReadRepository, BookReadRepository>()
        .AddScoped<IBookConsumer, BookConsumer>(); ;
    }
}