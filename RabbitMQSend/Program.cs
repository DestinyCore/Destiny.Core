using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQSend
{
    class Program
    {
        static void Main(string[] args)
        {


            for (int i = 0; i < 100; i++)
            {
                var k = i;
                Task.Run(()=>{
                    Task.Delay(1000);
                    Console.WriteLine("11313");
                    while (true)
                    {
                        if (k < 5)
                        {
                            Task.Run(() => {

                                Console.WriteLine("while");
                            });
                        }
                      
                    }
          
               
                });
            }
                
            //var factory = new ConnectionFactory() { HostName = "47.100.213.49",UserName= "DestinyMQ",Password= "P@ssW0rd",Port= 5672 };

            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        channel.QueueDeclare(queue: "hello",
            //                       durable: false,
            //                       exclusive: false,
            //                       autoDelete: false,
            //                       arguments: null);

            //        string message = "Hello World!";
            //        var body = Encoding.UTF8.GetBytes(message);

            //        channel.BasicPublish(exchange: "",
            //                             routingKey: "hello",
            //                             basicProperties: null,
            //                             body: body);
            //        Console.WriteLine(" [x] Sent {0}", message);

            //    }
            //}

            //Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
