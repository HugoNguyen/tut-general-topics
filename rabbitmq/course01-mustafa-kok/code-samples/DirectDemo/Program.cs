using RabbitMQ.Client;
using System.Text;

namespace DirectDemo
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
            await channel.ExchangeDeclareAsync("ex.direct", ExchangeType.Direct, durable: true, autoDelete: false);
            await channel.QueueDeclareAsync("my.infos", durable: true, exclusive: false, autoDelete: false);
            await channel.QueueDeclareAsync("my.warnings", durable: true, exclusive: false, autoDelete: false);
            await channel.QueueDeclareAsync("my.errors", durable: true, exclusive: false, autoDelete: false);

            //3. Binding exchange and queue
            await channel.QueueBindAsync("my.infos", "ex.direct", "info");
            await channel.QueueBindAsync("my.warnings", "ex.direct", "warning");
            await channel.QueueBindAsync("my.errors", "ex.direct", "error");

            //4. Publish messing
            await channel.BasicPublishAsync("ex.direct", "info", Encoding.UTF8.GetBytes("This is an info message"));
            await channel.BasicPublishAsync("ex.direct", "warning", Encoding.UTF8.GetBytes("This is a warning message"));
            await channel.BasicPublishAsync("ex.direct", "error", Encoding.UTF8.GetBytes("This is an error message"));

            Console.WriteLine("Press a key to exit.");
            Console.ReadKey();
            //5. Delete queue and exchange
            await channel.QueueDeleteAsync("my.infos");
            await channel.QueueDeleteAsync("my.warnings");
            await channel.QueueDeleteAsync("my.errors");
            await channel.ExchangeDeleteAsync("ex.direct");

            //6. Close channel and connection
            await channel.CloseAsync();
            await conn.CloseAsync();
            await channel.DisposeAsync();
            await conn.DisposeAsync();
        }
    }
}
