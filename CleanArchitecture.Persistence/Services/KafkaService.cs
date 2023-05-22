using CleanArchitecture.Application.Common.DTOs;
using CleanArchitecture.Domain.Services;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CleanArchitecture.Persistence.Services
{
    public class KafkaService : IKafkaService
    {
        private readonly ProducerConfig _config;

        private readonly string _topic;

        private readonly ILogger<KafkaService> _logger;

        public KafkaService(ILogger<KafkaService> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _config = new ProducerConfig { BootstrapServers = configuration.GetSection("Kafka:ConnectionString").Value };
            _topic = configuration.GetSection("Kafka:Topic").Value;
        }

        public async Task ProduceMessageAsync(string operation)
        {
            _logger.LogInformation($"Start ProduceMessage with kafka.");
            using (var producer = new ProducerBuilder<Null, string>(_config).Build())
            {
                OperationKafkaDto operationKafkaDto = new OperationKafkaDto
                {
                    Id = Guid.NewGuid(),
                    Operation = operation,
                };

                var responseProducer = await producer.ProduceAsync(_topic, 
                    new Message<Null, string> { Value = JsonSerializer.Serialize(operationKafkaDto) });
                
                if (responseProducer.Status != PersistenceStatus.Persisted)
                {
                    _logger.LogError($"Error with kafka. Status: {responseProducer.Status}");
                }

                _logger.LogInformation($"Message sent. Status: {responseProducer.Status}");
            }
        }
    }
}
