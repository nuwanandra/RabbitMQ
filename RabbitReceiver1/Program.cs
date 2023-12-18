using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;




namespace RabbitReceiver1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");

            
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://myuser:mypassword@localhost:5672");
            factory.ClientProvidedName = "Rabbit Receiver1 App";

            IConnection cnn = factory.CreateConnection();

            IModel channel = cnn.CreateModel();

            string exchangeName = "DemoExchange";
            string queueName = "DemoQueue";
            string routingKey = "demo-routing-key";


            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);
            channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, args) =>
            {

                Task.Delay(TimeSpan.FromSeconds(5)).Wait();


                var body = args.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Message Received:{message}");

                channel.BasicAck(args.DeliveryTag, false);
            };

            string consumerTag = channel.BasicConsume(queueName, false, consumer);
            Console.ReadLine();
            channel.BasicCancel(consumerTag);
            
            channel.Close();
            cnn.Close();

            


        }
    }
}
