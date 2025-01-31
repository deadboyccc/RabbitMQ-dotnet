using System;
using System.Text;
using RabbitMQ.Client;


//wrap in a an internal statement 

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

    // creating a queue
    await channel.Result.QueueDeclareAsync(queue: "first_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

    // creating the message
    var message = "test msg from .net";
    // encoding the message to bytes (stream of binary to stream/send)
    var encodedMsgBody = Encoding.UTF8.GetBytes(message);

    // we have to public to an exchange - "" = default exchange 
    await channel.Result.BasicPublishAsync("", "first_queue", encodedMsgBody);

    // confirm
    Console.WriteLine($" [x] Sent {message}");
  }
}