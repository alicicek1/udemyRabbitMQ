using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using static System.Console;

namespace subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://unpwxnla:4_fXpw3ZsSWd8xiDCK_t_J6lPvRtCNBE@elk.rmq2.cloudamqp.com/unpwxnla");

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //channel.QueueDeclare("hello-queue", true, false, false);

                channel.BasicQos(0, 1, false);
                var consumer = new EventingBasicConsumer(channel);

                channel.BasicConsume("hello-queue", false, consumer);

                consumer.Received += (object sender, BasicDeliverEventArgs e) =>
                {
                    var msg = Encoding.UTF8.GetString(e.Body.ToArray());

                    Thread.Sleep(1500);
                    WriteLine("Receiving message :" + msg);

                    channel.BasicAck(e.DeliveryTag, false);
                };

            }
            ReadLine();
        }
    }
}
