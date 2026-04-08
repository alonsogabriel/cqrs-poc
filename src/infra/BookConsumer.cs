using System.Text;
using System.Text.Json;
using CqrsPoc.App;
using CqrsPoc.App.Dtos;
using CqrsPoc.App.Models;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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
            var @event = JsonSerializer.Deserialize<Event<BookReadModel>>(json, jsonOptions);

            if (@event is null)
            {
                System.Console.WriteLine("Invalid message data.");
                return Task.CompletedTask;
            }

            if (!@event.Name.Equals("BookCreated"))
            {
                System.Console.WriteLine("Invalid event type.");
                return Task.CompletedTask;
            }

            if (@event.Data is null)
            {
                System.Console.WriteLine("Event data is null.");
                return Task.CompletedTask;
            }


            return OnBookCreated(@event.Data);
        };

        await channel.BasicConsumeAsync("create-book", autoAck: true, consumer: consumer, cancellationToken: ct);
    }

    public async Task OnBookCreated(BookReadModel data, CancellationToken ct = default)
    {
        await db.GetCollection<BookReadModel>("books").InsertOneAsync(data, null, ct);
    }
}