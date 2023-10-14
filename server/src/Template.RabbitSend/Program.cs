using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Template.Static;

namespace Template.RabbitSend
{
    class Program
    {
        private const string queueName = "omsv2_test";
        private const string exchangeName = "test_logs";
        static void Main(string[] args)
        {

            var timspan = TimeSpan.FromMinutes(480);
            var aa = timspan.Hours;
            var bb = timspan.Minutes;
            var cc = Convert.ToDateTime("2022-12-14T00:00:00Z");

            var factory = new ConnectionFactory()
            {
                HostName = "10.168.95.156",
                UserName = "OMSV2",
                Password = "OMSV2",
                VirtualHost = "/OMSV2/",
                Port = 5672,
                RequestedHeartbeat = TimeSpan.FromMinutes(1)
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "topic_logs",
                                        type: "topic");

                var routingKey = (args.Length > 0) ? args[0] : "anonymous.info";
                var message = (args.Length > 1)
                              ? string.Join(" ", args.Skip(1).ToArray())
                              : "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "topic_logs",
                                     routingKey: routingKey,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent '{0}':'{1}'", routingKey, message);
            }
        }
        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}
