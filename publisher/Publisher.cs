using System;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using static System.Console;

namespace publisher
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
                channel.QueueDeclare("hello-queue", true, false, false);

                Enumerable.Range(1, 50).ToList().ForEach(a =>
                {
                    string msg = a + ". Hello World!";

                    var body = Encoding.UTF8.GetBytes(msg);

                    channel.BasicPublish(String.Empty, "hello-queue", null, body);

                    WriteLine("Message has been sent. Message : " + msg);

                });


                ReadLine();
            }

        }
    }
}
