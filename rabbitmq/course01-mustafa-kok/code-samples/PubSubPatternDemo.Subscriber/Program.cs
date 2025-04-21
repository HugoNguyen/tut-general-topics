using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace PubSubPatternDemo.Subscriber
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
            var queueName = string.Empty;
            while(queueName == null || queueName == string.Empty)
            {
                Console.WriteLine("1) my.queue1");
                Console.WriteLine("2) my.queue2");
                Console.Write("Choose queue: ");
                var choosenNumber = Console.ReadLine();
                if (int.TryParse(choosenNumber, out int number))
                {
                    if (number == 1)
                    {
                        queueName = "my.queue1";
                        break;
                    }
                    else if (number == 2)
                    {
                        queueName = "my.queue2";
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input");
                        continue;
                    }
                }
            }

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Subscriber [{queueName}] Message: {message}");
                await Task.CompletedTask;
            };

            //3. Bind consumer to queue
            var consumerTag = await channel.BasicConsumeAsync(queueName, true, consumer);

            //4. Wait message
            Console.WriteLine($"Subcribe to the queue '{queueName}'. Press any key to exit.");
            Console.ReadKey();

            //5. Close channel and connection
            await channel.CloseAsync();
            await conn.CloseAsync();
        }
    }
}
