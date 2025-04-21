using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace PriorityQueueDemo.Worker
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

            //Don't send a new message to this worker
            //until it has processed and acknowkedged the last one
            await channel.BasicQosAsync(0, 1, false); // Limit prefetch count to 1. https://www.rabbitmq.com/docs/consumer-prefetch#overview

            //2. Create consumer
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                Console.Write($"Processing message -> '{message}'...");
                await Task.Delay(1000);
                Console.WriteLine("FINISHED");
                await channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            //3. Bind consumer to queue
            var consumerTag = await channel.BasicConsumeAsync(
                queue: "my.queue",
                autoAck: false, // let processor set ack when it done
                consumer: consumer);

            //4. Wait message
            Console.WriteLine("Subcribe to the queue. Press any key to exit.");
            Console.ReadKey();

            //5. Close channel and connection
            await channel.CloseAsync();
            await conn.CloseAsync();
        }
    }
}
