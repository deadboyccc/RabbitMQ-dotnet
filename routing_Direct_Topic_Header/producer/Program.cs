using System.Text;
using RabbitMQ.Client;


internal class RabbitMQSender
{

  public static async Task Main(string[] args)
  {
    // creating the connection factory to create our initial conneciton
    var factory = new ConnectionFactory { HostName = "localhost" };

    // creating a single connnection 
    using var connection = factory.CreateConnectionAsync();

    // creating a channel or multiple ones depending on the need
    using var channel = connection.Result.CreateChannelAsync();

    // Creating an exchange (fanout send to all binded queues)
    await channel.Result.ExchangeDeclareAsync(exchange: "pubsub", type: ExchangeType.Fanout);

    // creating a queue
    // await channel.Result.QueueDeclareAsync(queue: "first_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

    // Message id 
    uint messageId = 0;
    while (true)
    {
      var processingTime = new Random().Next(1, 3);
      // creating the message
      var message = $"{++messageId}: test msg from .net & took {processingTime}s to send";

      // encoding the message to bytes (stream of binary to stream/send)
      var encodedMsgBody = Encoding.UTF8.GetBytes(message);

      // we have to public to an exchange - "" = default exchange 
      await channel.Result.BasicPublishAsync(exchange: "pubsub", routingKey: "", body: encodedMsgBody);

      // confirm by logging to console
      Console.WriteLine($" [x] Sent {message} & took {processingTime}s to send");

      // simulate processing the message 
      await Task.Delay(TimeSpan.FromSeconds(processingTime));

    }
  }
}