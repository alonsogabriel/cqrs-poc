using System.Text;
using System.Text.Json;
using CqrsPoc.WriteApi.App;
using RabbitMQ.Client;

namespace CqrsPoc.Infra;

internal class BookProducer : IBookProducer
{
    public async Task<bool> SendAsync(object message, CancellationToken ct = default)
    {
        try
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync(ct);
            using var channel = await connection.CreateChannelAsync(null, ct);
            var args = new Dictionary<string, object?>
            {
                { "x-queue-type", "quorum" }
            };

            await channel.QueueDeclareAsync(
                queue: "create-book",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: args, cancellationToken: ct);

            var json = JsonSerializer.Serialize(message, new JsonSerializerOptions { WriteIndented = false });
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "create-book", body: body, cancellationToken: ct);
        }
        catch (Exception err)
        {
            Console.WriteLine(err.Message);
            return false;
        }

        return true;
    }
}