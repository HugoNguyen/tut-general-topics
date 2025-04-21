using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Collections.Concurrent;
using RequestResponsePatternDemo.Common;
using Newtonsoft.Json;

namespace RequestResponsePatternDemo.Requestor
{
    internal class Program
    {
        async static Task Main(string[] args)
        {
            ConcurrentDictionary<string, CalculationRequest> waitingRequest = new ConcurrentDictionary<string, CalculationRequest>();

            //1. Set up connection and channel
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.VirtualHost = "/";
            factory.Port = 5672;
            factory.UserName = "admin";
            factory.Password = "passw@rd";

            IConnection conn = await factory.CreateConnectionAsync();
            IChannel channel = await conn.CreateChannelAsync();

            var responseQueueName = "my.res." + Guid.NewGuid().ToString();
            await channel.QueueDeclareAsync(responseQueueName);

            //2. Create consumer
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (ch, ea) =>
            {
                var requestId = Encoding.UTF8.GetString((byte[])ea.BasicProperties.Headers![Common.Constants.RequestIdHeaderKey]!);
                CalculationRequest? request;
                if (waitingRequest.TryGetValue(requestId, out request))
                {
                    var body = ea.Body.ToArray();
                    string messageData = Encoding.UTF8.GetString(body);
                    var response = JsonConvert.DeserializeObject<CalculationResponse>(messageData);
                    Console.WriteLine($"Calculation result: {request} = {response}");
                }

                await Task.CompletedTask;
            };

            //3. Bind consumer to queue
            var consumerTag = await channel.BasicConsumeAsync(responseQueueName, true, consumer);

            //4. Send messages to response
            Console.WriteLine("Press a key to send requests");
            Console.ReadKey();

            await SendRequest(waitingRequest, channel, new CalculationRequest(2, 4, OperationType.Add), responseQueueName);
            await SendRequest(waitingRequest, channel, new CalculationRequest(16, 4, OperationType.Subtract), responseQueueName);
            await SendRequest(waitingRequest, channel, new CalculationRequest(50, 2, OperationType.Add), responseQueueName);
            await SendRequest(waitingRequest, channel, new CalculationRequest(30, 6, OperationType.Subtract), responseQueueName);

            Console.WriteLine($"Press any key to exit.");
            Console.ReadKey();

            //5. Close channel and connection
            await channel.QueueDeleteAsync(responseQueueName);
            await channel.CloseAsync();
            await conn.CloseAsync();
        }

        private static async Task SendRequest(
            ConcurrentDictionary<string, CalculationRequest> waitingRequest,
            IChannel channel,
            CalculationRequest request,
            string responseQueueName)
        {
            var requestId = Guid.NewGuid().ToString();
            var requestData = JsonConvert.SerializeObject(request);

            waitingRequest[requestId] = request;

            await channel.BasicPublishAsync(
                "",
                "my.requests",
                mandatory: true,
                basicProperties: new BasicProperties()
                {
                    Headers = new Dictionary<string, object?>()
                    {
                        { Common.Constants.RequestIdHeaderKey, Encoding.UTF8.GetBytes(requestId) },
                        { Common.Constants.ResponseQueueHeaderKey, Encoding.UTF8.GetBytes(responseQueueName) },
                    }
                },
                Encoding.UTF8.GetBytes(requestData));
        }
    }
}
