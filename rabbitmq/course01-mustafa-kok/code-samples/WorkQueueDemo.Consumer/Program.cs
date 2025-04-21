using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace WorkQueueDemo.Consumer
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

            await channel.BasicQosAsync(0, 1, false); // Limit prefetch count to 1. https://www.rabbitmq.com/docs/consumer-prefetch#overview

            //2. Create consumer
            Console.Write("Name of worker: ");
            var workerName = Console.ReadLine();

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                if (Int32.TryParse(message, out int durationInSecond))
                {
                    Console.Write($"[{workerName}] Task started. Duration: {durationInSecond}");
                    await Task.Delay(TimeSpan.FromSeconds(durationInSecond));
                    Console.WriteLine(" => Finished");
                }
                else
                {
                    Console.WriteLine($"Message: {message}");
                }
                await channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            //3. Bind consumer to queue
            var consumerTag = await channel.BasicConsumeAsync("my.queue1", false, consumer);

            //4. Wait message
            Console.WriteLine("Subcribe to the queue. Press any key to exit.");
            Console.ReadKey();

            //5. Close channel and connection
            await channel.CloseAsync();
            await conn.CloseAsync();
        }
    }
}
