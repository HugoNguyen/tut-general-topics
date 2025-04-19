using RabbitMQ.Client;
using System.Text;

namespace DefaultDemo
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
            await channel.QueueDeclareAsync("my.queue1", durable: true, exclusive: false, autoDelete: false);
            await channel.QueueDeclareAsync("my.queue2", durable: true, exclusive: false, autoDelete: false);

            //3. Binding exchange and queue

            //4. Publish messing
            await channel.BasicPublishAsync("", "my.queue1", Encoding.UTF8.GetBytes("This should go to my.queue1"));
            await channel.BasicPublishAsync("", "my.queue2", Encoding.UTF8.GetBytes("This should go to my.queue2"));

            Console.WriteLine("Press a key to exit.");
            Console.ReadKey();
            //5. Delete queue and exchange
            await channel.QueueDeleteAsync("my.queue1");
            await channel.QueueDeleteAsync("my.queue2");

            //6. Close channel and connection
            await channel.CloseAsync();
            await conn.CloseAsync();
            await channel.DisposeAsync();
            await conn.DisposeAsync();
        }
    }
}
