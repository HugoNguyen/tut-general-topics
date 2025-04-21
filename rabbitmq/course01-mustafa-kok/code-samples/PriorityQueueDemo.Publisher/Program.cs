using RabbitMQ.Client;
using System.Text;

namespace PriorityQueueDemo.Publisher
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

            //2. Create exchange and queue
            await channel.ExchangeDeclareAsync("ex.fanout", ExchangeType.Fanout, durable: true, autoDelete: false);

            //Create a queu that supports message priorities (with max message priority=5)
            var queueArguments = new Dictionary<string, object?>
            {
                { "x-max-priority", 2 }
            };
            
            await channel.QueueDeclareAsync("my.queue", durable: true, exclusive: false, autoDelete: false, arguments: queueArguments);

            //3. Binding exchange and queue
            await channel.QueueBindAsync("my.queue", "ex.fanout", "");

            //4. Publish messing
            Console.WriteLine("Publisher is ready. Press a key to start sending messages");
            Console.ReadKey();

            //Send sample message with low (priority=1) and high (priority=5) priorities

            //Low priority messages
            await sendMessage(channel, 1);
            await sendMessage(channel, 1);
            await sendMessage(channel, 1);

            //High priority messages
            await sendMessage(channel, 2);
            await sendMessage(channel, 2);

            Console.WriteLine("Press a key to exit");
            Console.ReadKey();

            //5. Delete queue and exchange
            await channel.QueueDeleteAsync("my.queue");
            await channel.ExchangeDeleteAsync("ex.fanout");

            //6. Close channel and connection
            await channel.CloseAsync();
            await conn.CloseAsync();
            await channel.DisposeAsync();
            await conn.DisposeAsync();
        }

        private static async Task sendMessage(IChannel channel, byte priority)
        {
            var message = $"Message with priority={priority}";
            await channel.BasicPublishAsync(
                exchange: "ex.fanout",
                routingKey: "",
                mandatory: true,
                basicProperties: new BasicProperties
                {
                    Priority = priority,
                },
                body: Encoding.UTF8.GetBytes(message));
            Console.WriteLine($"SENT: {message}");
        }
    }
}
