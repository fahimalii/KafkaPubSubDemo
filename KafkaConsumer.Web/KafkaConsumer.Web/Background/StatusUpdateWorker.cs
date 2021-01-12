﻿using Confluent.Kafka;
using KafkaConsumer.Web.GlobalVariables;
using System.Diagnostics;
using System.Threading;

namespace KafkaConsumer.Web.Background
{
    public class StatusUpdateWorker
    {
        private readonly IConsumer<Ignore, string> _consumer;

        public StatusUpdateWorker()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "console-demo1",
                AutoOffsetReset = AutoOffsetReset.Latest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        }

        public void StartProcessing(CancellationToken cancellationToken = default(CancellationToken))
        {
            Debug.WriteLine($"Subscriping to topic -> demo");
            _consumer.Subscribe("demo");

            while (!cancellationToken.IsCancellationRequested)
            {
                var consumeResult = _consumer.Consume(cancellationToken);
                var message = consumeResult.Message.Value;
                Debug.WriteLine($"Received Message : {message}");

                // Write in the global shared variable
                Status.Message = message;
                Status.MessagesList.Add(message);
            }

            _consumer.Close();
        }
    }
}