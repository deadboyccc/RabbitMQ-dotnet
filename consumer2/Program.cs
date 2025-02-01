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

    // setting channel settings to test competing consumer pattern
    await channel.Result.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

    // Create consumer
    var consumer = new AsyncEventingBasicConsumer(channel.Result);

    consumer.ReceivedAsync += async (model, ea) =>
    {

      var processingTime = new Random().Next(1, 5);
      // Simulate processing the message
      var body = ea.Body.ToArray();
      var message = Encoding.UTF8.GetString(body);
      Console.WriteLine($" [x] Received {message} | Took {processingTime}s to recieve");

      // Simulating waiting time to examine the competing consumers pattern
      // between 1-4 seconds
      await Task.Delay(TimeSpan.FromSeconds(processingTime)); // Simulate waiting time
      await channel.Result.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
    };

    // Consume messages
    await channel.Result.BasicConsumeAsync(queue: "first_queue", autoAck: false, consumer: consumer);

    Console.WriteLine(" [*] Waiting for messages. Press [enter] to exit.");
    Console.ReadLine(); // Keep the program running
  }
}

