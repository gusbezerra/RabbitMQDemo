
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

IConfiguration configuration = new ConfigurationBuilder()
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

//processa uma mensagem por vez
await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false); 
Console.WriteLine(" [*] Receiver1 aguardando mensagens...");

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (sender, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Receiver1 recebeu: {message}");

    //simular processamento
    Task.Delay(TimeSpan.FromSeconds(2)).GetAwaiter().GetResult();

    //cofirmação manual
    await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
};

await channel.BasicConsumeAsync(queue: "message_queue", autoAck: false, consumer: consumer);

Console.WriteLine("Pressione qualquer tecla para sair...");
Console.ReadKey();