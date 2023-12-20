using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public static class HeaderExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            string routingKey = String.Empty;

            string exchangeName = "demo-header-exchange";
            channel.ExchangeDeclare(exchangeName, ExchangeType.Headers);

            string queueName = "demo-header-queue";
            channel.QueueDeclare(queueName, true, false, false, null);

            var header = new Dictionary<string, object> { { "account", "new" } };

            channel.QueueBind(queueName, exchangeName, routingKey, header);
            channel.BasicQos(0, 10, false);


          


            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, args) =>
            {
                Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                var body = args.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);

            };

            channel.BasicConsume(queueName, true, consumer);
            Console.WriteLine("Consumer Started");
            Console.ReadLine();
            channel.Close();

        }


    }
}
