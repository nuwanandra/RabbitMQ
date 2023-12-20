using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public static class DirectExchangeProducer
    {

        public static void Publish(IModel channel)
        {
            //string queueName = "demo-queue";
            //channel.QueueDeclare(queueName, true, false, false, null);

            var ttl = new Dictionary<string, object>()
            {
                {"x-message-ttl", 30000 }
            };



            string routingKey = "account.init";
            string exchangeName = "demo-direct-exchange";
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, arguments:ttl);


            var count = 0;
            var max = 1000;

            while (count < max)
            {
                var message = new { Name = "Producer", Messagee = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                channel.BasicPublish(exchangeName, routingKey, null, body);
                count++;
                Thread.Sleep(1000);
            }

            channel.Close();


        }



    }
}
