using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace PushAndPullDemo.PullConsumer
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

            //3. Bind consumer to queue

            //4. Wait message
            Console.WriteLine("Reading messages from queue. Press any key to exit.");

            while (true)
            {
                Console.WriteLine("Trying to get a message from the queue...");

                BasicGetResult? result = await channel.BasicGetAsync("my.queue1", true);

                if (result != null)
                {
                    var body = result.Body.ToArray();
                    string message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Message: {message}");

                    if (Console.KeyAvailable)
                    {
                        var keyInfo = Console.ReadKey();
                        if (keyInfo.KeyChar == 'e' || keyInfo.KeyChar == 'E')
                        {
                            break;
                        }
                    }
                }

                await Task.Delay(2000);
            }

            //5. Close channel and connection
            await channel.CloseAsync();
            await conn.CloseAsync();
        }
    }
}
