using RabbitMQ.Client;
using System.Text;

namespace ExToExDemo
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
            await channel.ExchangeDeclareAsync("exchange1", ExchangeType.Direct, durable: true, autoDelete: false);
            await channel.ExchangeDeclareAsync("exchange2", ExchangeType.Direct, durable: true, autoDelete: false);
            await channel.QueueDeclareAsync("queue1", durable: true, exclusive: false, autoDelete: false);
            await channel.QueueDeclareAsync("queue2", durable: true, exclusive: false, autoDelete: false);

            //3. Binding exchange and queue
            await channel.QueueBindAsync("queue1", "exchange1", "key1");
            await channel.QueueBindAsync("queue2", "exchange2", "key2");
            await channel.ExchangeBindAsync(
                destination: "exchange2",
                source: "exchange1",
                routingKey: "key2");

            //4. Publish messing
            await channel.BasicPublishAsync("exchange1", "key1", Encoding.UTF8.GetBytes("This is the first message published to exchange1 with key key1"));
            await channel.BasicPublishAsync("exchange1", "key2", Encoding.UTF8.GetBytes("This is the second message that is original published to exchange1 with key key2"));

            Console.WriteLine("Press a key to exit.");
            Console.ReadKey();
            //5. Delete queue and exchange
            await channel.QueueDeleteAsync("queue1");
            await channel.QueueDeleteAsync("queue2");
            await channel.ExchangeDeleteAsync("exchange1");
            await channel.ExchangeDeleteAsync("exchange2");

            //6. Close channel and connection
            await channel.CloseAsync();
            await conn.CloseAsync();
            await channel.DisposeAsync();
            await conn.DisposeAsync();
        }
    }
}
