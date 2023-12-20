using RabbitMQ.Client;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using RabbitMQ.Client.Events;


namespace RabbitMQ.Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://myuser:mypassword@localhost:5672");
            factory.ClientProvidedName = "Rabbit Cousumer1 App";

            IConnection cnn = factory.CreateConnection();
            IModel channel = cnn.CreateModel();

            //QueueConsumer.Consume(channel);
            FanoutExchangeConsumer.Consume(channel);

            //string exchangeName = "DemoExchange";
            
            cnn.Close();           
            Console.ReadLine();
        }
    }
}
