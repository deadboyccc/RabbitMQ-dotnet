using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Text;

public class RpcClient : IAsyncDisposable
{
    private const string QUEUE_NAME = "rpc_queue";

    private readonly IConnectionFactory connectionFactory;
    private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> callbackMapper
        = new();

    private IConnection? connection;
    private IChannel? channel;
    private string? replyQueueName;

    public RpcClient()
    {
        connectionFactory = new ConnectionFactory { HostName = "localhost" };
    }

    public async Task StartAsync()
    {
        connection = await connectionFactory.CreateConnectionAsync();
        channel = await connection.CreateChannelAsync();

        QueueDeclareOk queueDeclareResult = await channel.QueueDeclareAsync();
        replyQueueName = queueDeclareResult.QueueName;
        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += (model, ea) =>
        {
            string? correlationId = ea.BasicProperties.CorrelationId;

            if (false == string.IsNullOrEmpty(correlationId))
            {
                if (callbackMapper.TryRemove(correlationId, out var tcs))
                {
                    var body = ea.Body.ToArray();
                    var response = Encoding.UTF8.GetString(body);
                    tcs.TrySetResult(response);
                }
            }

            return Task.CompletedTask;
        };

        await channel.BasicConsumeAsync(replyQueueName, true, consumer);
    }

    public async Task<string> CallAsync(string message,
        CancellationToken cancellationToken = default)
    {
        if (channel is null)
        {
            throw new InvalidOperationException();
        }

        string correlationId = Guid.NewGuid().ToString();
        var props = new BasicProperties
        {
            CorrelationId = correlationId,
            ReplyTo = replyQueueName
        };

        var tcs = new TaskCompletionSource<string>(
            TaskCreationOptions.RunContinuationsAsynchronously);
        callbackMapper.TryAdd(correlationId, tcs);

        var messageBytes = Encoding.UTF8.GetBytes(message);
        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: QUEUE_NAME,
            mandatory: true, basicProperties: props, body: messageBytes);

        using CancellationTokenRegistration ctr =
            cancellationToken.Register(() =>
            {
                callbackMapper.TryRemove(correlationId, out _);
                tcs.SetCanceled();
            });

        return await tcs.Task;
    }

    public async ValueTask DisposeAsync()
    {
        if (channel is not null)
        {
            await channel.CloseAsync();
        }

        if (connection is not null)
        {
            await connection.CloseAsync();
        }
    }
}

public class Rpc
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("RPC Client");
        string n = args.Length > 0 ? args[0] : "30";
        await InvokeAsync(n);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }

    private static async Task InvokeAsync(string n)
    {
        var rpcClient = new RpcClient();
        await rpcClient.StartAsync();

        Console.WriteLine(" [x] Requesting fib({0})", n);
        var response = await rpcClient.CallAsync(n);
        Console.WriteLine(" [.] Got '{0}'", response);
    }
}