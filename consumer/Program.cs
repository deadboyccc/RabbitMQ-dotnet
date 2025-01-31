using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

internal class RabbitMQConsumer
{
  public static async Task Main(string[] args)
  {
    var factory = new ConnectionFactory { HostName = "localhost" };

    // Create connection (single connection for performance)
    using var connection = factory.CreateConnectionAsync();

    // Create channel (single channel for simplicity)
    using var channel = connection.Result.CreateChannelAsync();

    // Declare queue to consume from
    await channel.Result.QueueDeclareAsync(queue: "first_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

    // Create consumer
    var consumer = new AsyncEventingBasicConsumer(channel.Result);

    consumer.ReceivedAsync += async (model, ea) =>
    {
      var body = ea.Body.ToArray();
      var message = Encoding.UTF8.GetString(body);
      Console.WriteLine($" [x] Received {message}");

      await Task.Yield(); // Simulate async work
    };

    // Consume messages
    await channel.Result.BasicConsumeAsync(queue: "first_queue", autoAck: true, consumer: consumer);

    Console.WriteLine(" [*] Waiting for messages. Press [enter] to exit.");
    Console.ReadLine(); // Keep the program running
  }
}
