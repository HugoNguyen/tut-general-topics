using RabbitMQ.Client;
using System.Text;

namespace AlternateDemo
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
            await channel.ExchangeDeclareAsync("ex.direct", ExchangeType.Direct, durable: true, autoDelete: false, arguments: new Dictionary<string, object?>
            {
                { "alternate-exchange" , "ex.fanout" }
            });
            await channel.QueueDeclareAsync("my.queue1", durable: true, exclusive: false, autoDelete: false);
            await channel.QueueDeclareAsync("my.queue2", durable: true, exclusive: false, autoDelete: false);
            await channel.QueueDeclareAsync("my.unrouted", durable: true, exclusive: false, autoDelete: false);

            //3. Binding exchange and queue
            await channel.QueueBindAsync("my.queue1", "ex.direct", "video");
            await channel.QueueBindAsync("my.queue2", "ex.direct", "image");
            await channel.QueueBindAsync("my.unrouted", "ex.fanout", "");

            //4. Publish messing
            await channel.BasicPublishAsync("ex.direct", "video", Encoding.UTF8.GetBytes("This message has a routing key \"video\""));
            await channel.BasicPublishAsync("ex.direct", "image", Encoding.UTF8.GetBytes("This message has a routing key \"image\""));
            await channel.BasicPublishAsync("ex.direct", "text", Encoding.UTF8.GetBytes("This message is unrouted since there is no queue bound with this key"));

            Console.WriteLine("Press a key to exit.");
            Console.ReadKey();
            //5. Delete queue and exchange
            await channel.QueueDeleteAsync("my.queue1");
            await channel.QueueDeleteAsync("my.queue2");
            await channel.QueueDeleteAsync("my.unrouted");
            await channel.ExchangeDeleteAsync("ex.fanout");
            await channel.ExchangeDeleteAsync("ex.direct");

            //6. Close channel and connection
            await channel.CloseAsync();
            await conn.CloseAsync();
            await channel.DisposeAsync();
            await conn.DisposeAsync();
        }
    }
}
