using RabbitMQ.Client;
using System.Text;

namespace TopicDemo
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
            await channel.ExchangeDeclareAsync("ex.topic", ExchangeType.Topic, durable: true, autoDelete: false);
            await channel.QueueDeclareAsync("my.queue1", durable: true, exclusive: false, autoDelete: false);
            await channel.QueueDeclareAsync("my.queue2", durable: true, exclusive: false, autoDelete: false);
            await channel.QueueDeclareAsync("my.queue3", durable: true, exclusive: false, autoDelete: false);

            //3. Binding exchange and queue
            await channel.QueueBindAsync("my.queue1", "ex.topic", "*.image.*");
            await channel.QueueBindAsync("my.queue2", "ex.topic", "#.image");
            await channel.QueueBindAsync("my.queue3", "ex.topic", "image.#");

            //4. Publish messing
            await channel.BasicPublishAsync("ex.topic", "convert.image.bmp", Encoding.UTF8.GetBytes("Routing key is convert.image.bmp")); // Debug, check my.queue1
            await channel.BasicPublishAsync("ex.topic", "convert.bitmap.image", Encoding.UTF8.GetBytes("Routing key is convert.bitmap.image")); // check my.queue2
            await channel.BasicPublishAsync("ex.topic", "image.bitmap.32bit", Encoding.UTF8.GetBytes("Routing key is image.bitmap.32bit")); // check my.queue3
            await channel.BasicPublishAsync("ex.topic", "image", Encoding.UTF8.GetBytes("Routing key is only image")); // check my.queue2 + my.queue3

            Console.WriteLine("Press a key to exit.");
            Console.ReadKey();
            //5. Delete queue and exchange
            await channel.QueueDeleteAsync("my.queue1");
            await channel.QueueDeleteAsync("my.queue2");
            await channel.QueueDeleteAsync("my.queue3");
            await channel.ExchangeDeleteAsync("ex.topic");

            //6. Close channel and connection
            await channel.CloseAsync();
            await conn.CloseAsync();
            await channel.DisposeAsync();
            await conn.DisposeAsync();
        }
    }
}
