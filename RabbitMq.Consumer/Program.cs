

using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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


var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (sender, ea) =>
{
    byte[] body = ea.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");

    await ((AsyncEventingBasicConsumer)sender).Channel.BasicAckAsync(ea.DeliveryTag, multiple: false);


};

await channel.BasicConsumeAsync("message_queue", autoAck: false, consumer);

Console.ReadLine(); // Keeps the app running

