using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bots.Messaging.Receive.Options.v1;
using Bots.Service.Models;
using Bots.Service.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Bots.Messaging.Receive.Receiver.v1
{
    public class BotnetStatusUpdateReceiver : BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly IBotnetStatusUpdateService _botnetStatusUpdateService;
        private readonly string _hostName;
        private readonly string _queueName;
        private readonly string _userName;
        private readonly string _password;

        public BotnetStatusUpdateReceiver(IBotnetStatusUpdateService botnetStatusUpdateService,
            IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostName = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
            _userName = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _botnetStatusUpdateService = botnetStatusUpdateService;
            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password
            };

            bool connected = false;

            while (!connected)
            {
                try
                {

                    _connection = factory.CreateConnection();
                    _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                    _channel = _connection.CreateModel();
                    _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false,
                        arguments: null);
                    connected = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Can't connect to RabbitMQ, retrying...");
                    Thread.Sleep(5000);
                }
            }
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var updateBotnetStatusModel = JsonConvert.DeserializeObject<UpdateBotnetStatusModel>(content);

                HandleMessage(updateBotnetStatusModel);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }


        private void HandleMessage(UpdateBotnetStatusModel updateBotnetStatusModel)
        {
            _botnetStatusUpdateService.UpdateBotnetStatus(updateBotnetStatusModel);
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
