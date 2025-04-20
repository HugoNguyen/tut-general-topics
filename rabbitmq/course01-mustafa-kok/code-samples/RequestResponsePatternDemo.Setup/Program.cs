using RabbitMQ.Client;

namespace RequestResponsePatternDemo.Setup
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
            await channel.QueueDeclareAsync("my.requests", durable: true, exclusive: false, autoDelete: false);
            //Each requestor will have it own response queue
            //await channel.QueueDeclareAsync("my.responses", durable: true, exclusive: false, autoDelete: false);

            //3. Binding exchange and queue

            //4. Publish messing
            Console.WriteLine("Setup completed.");
            Console.WriteLine("Press a key to exit.");
            Console.ReadKey();

            //5. Delete queue and exchange
            await channel.QueueDeleteAsync("my.requests");
            //await channel.QueueDeleteAsync("my.responses");

            //6. Close channel and connection
            await channel.CloseAsync();
            await conn.CloseAsync();
            await channel.DisposeAsync();
            await conn.DisposeAsync();
        }
    }
}
