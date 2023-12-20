using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public static class HeaderExchangeProducer
    {

        public static void Publish(IModel channel)
        {
            //string queueName = "demo-queue";
            //channel.QueueDeclare(queueName, true, false, false, null);

            var ttl = new Dictionary<string, object>()
            {
                {"x-message-ttl", 30000 }
            };



            string routingKey = String.Empty;
            string exchangeName = "demo-header-exchange";
            channel.ExchangeDeclare(exchangeName, ExchangeType.Headers, arguments: ttl);


            var count = 0;
            var max = 1000;

            while (count < max)
            {
                var message = new { Name = "Producer", Messagee = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> { { "account", "new" } };


                channel.BasicPublish(exchangeName, routingKey, properties, body);
                count++;
                Console.WriteLine($"Message: {count}");
                Thread.Sleep(1000);
            }

            channel.Close();


        }


    }
}
