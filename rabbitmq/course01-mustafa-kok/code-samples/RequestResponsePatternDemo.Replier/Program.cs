using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using RequestResponsePatternDemo.Common;

namespace RequestResponsePatternDemo.Replier
{
    internal class Program
    {
        async static Task Main(string[] args)
        {
            //1. Set up connection and channel
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.VirtualHost = "/";
            factory.Port = 5672;
            factory.UserName = "admin";
            factory.Password = "passw@rd";

            IConnection conn = await factory.CreateConnectionAsync();
            IChannel channel = await conn.CreateChannelAsync();

            //2. Create consumer
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                string requestData = Encoding.UTF8.GetString(body);
                var request = JsonConvert.DeserializeObject<CalculationRequest>(requestData);

                if (request == null)
                {
                    return;
                }

                Console.WriteLine($"Request received: {request}");

                var response = new CalculationResponse();
                if (request.Operation == OperationType.Add)
                {
                    response.Result = request.Number1 + request.Number2;
                }
                else if (request.Operation == OperationType.Subtract)
                {
                    response!.Result = request.Number1 - request.Number2;
                }

                var responseData = JsonConvert.SerializeObject(response);

                await channel.BasicPublishAsync(
                "",
                "my.responses",
                mandatory: true,
                basicProperties: new BasicProperties()
                {
                    Headers = new Dictionary<string, object?>()
                    {
                        { Common.Constants.RequestIdHeaderKey, ea.BasicProperties.Headers![Common.Constants.RequestIdHeaderKey] },
                    }
                },
                Encoding.UTF8.GetBytes(responseData));
            };

            //3. Bind consumer to queue
            var consumerTag = await channel.BasicConsumeAsync("my.requests", true, consumer);

            //4. Wait message
            Console.WriteLine($"Press any key to exit.");
            Console.ReadKey();

            //5. Close channel and connection
            await channel.CloseAsync();
            await conn.CloseAsync();
        }
    }
}
