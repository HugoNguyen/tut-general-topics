using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

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
                string request = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Request received: {request}");

                var response = $"Response for {request}";

                await channel.BasicPublishAsync("", "my.responses", Encoding.UTF8.GetBytes(response));
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
