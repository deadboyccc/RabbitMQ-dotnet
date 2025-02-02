﻿using System.Text;
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

    //we declare the exchange here to incase the consumer starts first, it will initiate the channel
    // if channel already exists == no problem 
    await channel.Result.ExchangeDeclareAsync(exchange: "DirectRouting", ExchangeType.Direct);

    //declare temp queue to consume from
    var queueName = await channel.Result.QueueDeclareAsync();



    await channel.Result.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

    //binding the channel 
    await channel.Result.QueueBindAsync(queueName, exchange: "DirectRouting", routingKey: "payments");

    // Create consumer 
    var consumer = new AsyncEventingBasicConsumer(channel.Result);



    // listener - event happens when a msg recieved model = consumer instance
    consumer.ReceivedAsync += async (model, msg) =>
    {

      var processingTime = new Random().Next(1, 5);
      // Simulate processing the message
      var body = msg.Body.ToArray();
      var message = Encoding.UTF8.GetString(body);
      Console.WriteLine($" [Payments] Received {message} | Took {processingTime}s to recieve");

      await Task.Delay(TimeSpan.FromSeconds(processingTime)); // Simulate waiting time
      // manual ack
      await channel.Result.BasicAckAsync(deliveryTag: msg.DeliveryTag, multiple: false);
    };

    // Start Readind/Consuming messages from the queue 
    await channel.Result.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);

    Console.WriteLine(" [Payments] Waiting for messages. Press [enter] to exit.");
    Console.ReadLine(); // Keep the program running
  }
}
