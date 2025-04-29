using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var factory = new ConnectionFactory
{
    HostName = configuration["RabbitMQ:Host"]!,
    UserName = configuration["RabbitMQ:Username"]!,
    Password = configuration["RabbitMQ:Password"]!
};

using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.ExchangeDeclareAsync(
    exchange: "message_exchange",
    type: ExchangeType.Direct, 
    durable: true);

await channel.QueueDeclareAsync(
    queue: "message_queue",
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null);

await channel.QueueBindAsync(
    queue: "message_queue",
    exchange: "message_exchange",
    routingKey: "message_key");

for (int i = 1; i <= 60; i++)
{
    string message = $"Mensagem número {i}";
    var body = Encoding.UTF8.GetBytes(message);

    await channel.BasicPublishAsync(
        exchange: "message_exchange",
        routingKey: "message_key",
        mandatory: true,
        basicProperties: new BasicProperties { Persistent = true },
        body);

    Console.WriteLine($" [x] Enviada: {message}");
}

Console.WriteLine("Pressione qualquer tecla para sair...");
Console.ReadKey();