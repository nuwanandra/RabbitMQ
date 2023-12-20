using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public class QueueConsumer
    {

        public static void Consume(IModel channel)
        {
            string queueName = "demo-queue";
            //string routingKey = "demo-routing-key";
            //channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, true, false, false, null);
            //channel.QueueBind(queueName, exchangeName, routingKey, null);


            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, args) =>
            {               
                var body = args.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);               
                Console.WriteLine(message);            

            };

            channel.BasicConsume(queueName, true, consumer);
            Console.WriteLine("Consumer Started");
            Console.ReadLine();
            channel.Close();


            //Task.Delay(TimeSpan.FromSeconds(5)).Wait();
            //var ob1= JsonSerializer.Deserialize()
            //channel.BasicAck(args.DeliveryTag, false);
            //string consumerTag = channel.BasicConsume(queueName, false, consumer);
            //Console.ReadLine();
            //channel.BasicCancel(consumerTag);




        }


    }
}
