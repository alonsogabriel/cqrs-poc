using System.Text;
using System.Text.Json;
using CqrsPoc.App.Events;
using CqrsPoc.ReadApi.App.Models;
using Microsoft.AspNetCore.Components;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CqrsPoc.ReadApi.App;

public interface IBookConsumer : IDisposable
{
    Task Init(CancellationToken ct = default);
    Task OnBookCreated(BookCreated data, CancellationToken ct = default);
}

internal class BookConsumer(IMongoDatabase db) : IBookConsumer, IAsyncDisposable
{
    private IConnection? connection;
    private IChannel? channel;

    public void Dispose()
    {
        DisposeAsync().GetAwaiter().GetResult();
    }

    public async ValueTask DisposeAsync()
    {
        if (channel is not null)
        {
            await channel.DisposeAsync();
        }
        if (connection is not null)
        {
            await connection.DisposeAsync();
        }
    }

    public async Task Init(CancellationToken ct = default)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        connection = await factory.CreateConnectionAsync(ct);
        channel = await connection.CreateChannelAsync(null, ct);
        var args = new Dictionary<string, object?> {
            { "x-queue-type", "quorum" }
        };

        await channel.QueueDeclareAsync(
            queue: "create-book",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: args,
            cancellationToken: ct);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var data = JsonSerializer.Deserialize<BookCreated>(json, jsonOptions);

            return OnBookCreated(data!);
        };

        await channel.BasicConsumeAsync("create-book", autoAck: true, consumer: consumer, cancellationToken: ct);
    }

    public async Task OnBookCreated(BookCreated data, CancellationToken ct = default)
    {
        var book = new Book
        {
            Id = data.Id,
            Name = data.Name,
            Authors = data.Authors,
            Category = new() { Id = data.Id, Name = data.Name },
            CreatedAt = data.CreatedAt,
            UpdatedAt = null
        };

        await db.GetCollection<Book>("books").InsertOneAsync(book, null, ct);
    }
}