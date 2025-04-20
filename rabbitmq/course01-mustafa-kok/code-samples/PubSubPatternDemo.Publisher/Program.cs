using RabbitMQ.Client;
using System.Text;

namespace PubSubPatternDemo.Publisher
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
            await channel.QueueDeclareAsync("my.queue1", durable: true, exclusive: false, autoDelete: false);
            await channel.QueueDeclareAsync("my.queue2", durable: true, exclusive: false, autoDelete: false);

            //3. Binding exchange and queue
            await channel.QueueBindAsync("my.queue1", "ex.fanout", "");
            await channel.QueueBindAsync("my.queue2", "ex.fanout", "");

            //4. Publish messing
            while (true)
            {
                Console.Write("Press 'exit' to exit.Or Enter message: ");
                var message = Console.ReadLine();
                if (message == null || message.Trim() == string.Empty)
                {
                    continue;
                }
                if (message == "exit")
                {
                    break;
                }
                await channel.BasicPublishAsync("ex.fanout", "", Encoding.UTF8.GetBytes(message));
            }

            //5. Delete queue and exchange
            await channel.QueueDeleteAsync("my.queue1");
            await channel.QueueDeleteAsync("my.queue2");
            await channel.ExchangeDeleteAsync("ex.fanout");

            //6. Close channel and connection
            await channel.CloseAsync();
            await conn.CloseAsync();
            await channel.DisposeAsync();
            await conn.DisposeAsync();
        }
    }
}
