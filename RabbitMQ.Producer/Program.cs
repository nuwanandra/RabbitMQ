using RabbitMQ.Client;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;


namespace RabbitMQ.Producer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://myuser:mypassword@localhost:5672");
            factory.ClientProvidedName = "Rabbit Producer App";

            IConnection cnn = factory.CreateConnection();
            IModel channel = cnn.CreateModel();
            
            //QueueProducer.Publish(channel);
            FanoutExchangeProducer.Publish(channel);
            cnn.Close();

            Console.ReadLine();


            //string exchangeName = "DemoExchange";
            //string queueName = "demo-queue";
            //string routingKey = "demo-routing-key";


            //channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            //channel.QueueDeclare(queueName, true, false, false, null);
            //channel.QueueBind(queueName, exchangeName, routingKey, null);

            //var message = new { Name="Producer", Messagee="Hello!"};
            //var messageBodyBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            //channel.BasicPublish("", queueName, null, messageBodyBytes);

            //for (int i = 0; i < 60; i++)
            //{
            //    Console.WriteLine($"Sending Message: {i}");

            //    byte[] messageBodyBytes = Encoding.UTF8.GetBytes($"Message #{i}");
            //    //channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);
            //    channel.BasicPublish("", queueName, null, messageBodyBytes);
            //    Thread.Sleep(1000);

            //}





        }
    }
}
