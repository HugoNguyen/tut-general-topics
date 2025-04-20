using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace RequestResponsePatternDemo.Requestor
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
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Response received: {message}");
                await Task.CompletedTask;
            };

            //3. Bind consumer to queue
            var consumerTag = await channel.BasicConsumeAsync("my.responses", true, consumer);

            //4. Send messages to response
            while (true)
            {
                Console.Write("Enter your request: ");
                var request = Console.ReadLine();

                if (request == null || request == string.Empty)
                {
                    continue;
                }

                if (request == "exit")
                {
                    break;
                }

                await channel.BasicPublishAsync("", "my.requests", Encoding.UTF8.GetBytes(request));
            }

            //5. Close channel and connection
            await channel.CloseAsync();
            await conn.CloseAsync();
        }
    }
}
