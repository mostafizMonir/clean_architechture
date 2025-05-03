
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory()
{
    HostName = "rabbitmq",
    Port = 5672,
    UserName = "guest",
    Password = "guest"
};
using IConnection connection = await factory.CreateConnectionAsync();
using IChannel channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(
    queue: "message_queue",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null
    );

for (int i = 0; i < 2; i++)
{
    string msg = $"{DateTime.UtcNow} - {Guid.CreateVersion7()}";
    byte[] body = Encoding.UTF8.GetBytes(msg);

    await channel.BasicPublishAsync(
        exchange: string.Empty,
        routingKey: "message_queue",
        mandatory: true,
        basicProperties: new BasicProperties { Persistent = true },
        body: body

    );
    Console.WriteLine($" [x] Sent {body}");
    await Task.Delay(2000);
}
