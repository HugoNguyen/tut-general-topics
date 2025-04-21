using RabbitMQ.Client;
using System;
using System.Text;

namespace HeadersDemo
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
            await channel.ExchangeDeclareAsync("ex.headers", ExchangeType.Headers, durable: true, autoDelete: false);
            await channel.QueueDeclareAsync("my.queue1", durable: true, exclusive: false, autoDelete: false);
            await channel.QueueDeclareAsync("my.queue2", durable: true, exclusive: false, autoDelete: false);

            //3. Binding exchange and queue
            await channel.QueueBindAsync("my.queue1", "ex.headers", "", arguments: new Dictionary<string, object?>()
            {
                { "x-match", "any" },
                { "job", "convert" },
                { "format", "jpeg" }
            });
            await channel.QueueBindAsync("my.queue2", "ex.headers", "", arguments: new Dictionary<string, object?>()
            {
                { "x-match", "all" },
                { "job", "convert" },
                { "format", "jpeg" }
            });

            //4. Publish messing
            await channel.BasicPublishAsync(
                "ex.headers",
                "",
                mandatory: true,
                basicProperties: new BasicProperties()
                {
                    Headers = new Dictionary<string, object?>()
                    {
                        { "job", "convert" },
                        { "format", "jpeg" }
                    }
                },
                Encoding.UTF8.GetBytes("This message has both of the expected header value"));

            await channel.BasicPublishAsync(
                "ex.headers",
                "",
                mandatory: true,
                basicProperties: new BasicProperties()
                {
                    Headers = new Dictionary<string, object?>()
                    {
                        { "job", "convert" },
                        { "format", "bmp" }
                    }
                },
                Encoding.UTF8.GetBytes("This message has only one matching header value"));

            Console.WriteLine("Press a key to exit.");
            Console.ReadKey();
            //5. Delete queue and exchange
            await channel.QueueDeleteAsync("my.queue1");
            await channel.QueueDeleteAsync("my.queue2");
            await channel.ExchangeDeleteAsync("ex.headers");

            //6. Close channel and connection
            await channel.CloseAsync();
            await conn.CloseAsync();
            await channel.DisposeAsync();
            await conn.DisposeAsync();
        }
    }
}
