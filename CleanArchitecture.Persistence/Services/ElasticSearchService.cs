using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Services;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nest;

namespace CleanArchitecture.Persistence.Services
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly ILogger<ElasticSearchService> _logger;
        private readonly IElasticClient _client;
        private readonly string _elasticIndex;

        public ElasticSearchService(ILogger<ElasticSearchService> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _elasticIndex = configuration.GetSection("Elastic:ElasticIndex").Value;

            var nodes = new Uri[]
            {
                new Uri(configuration.GetSection("Elastic:ConnectionString").Value)
            };

            var connectionPool = new SniffingConnectionPool(nodes);
            var connectionSettings = new ConnectionSettings(connectionPool)
                                .SniffOnConnectionFault(false)
                                .SniffOnStartup(false)
                                .SniffLifeSpan(TimeSpan.FromMinutes(1));

            _client = new ElasticClient(connectionSettings);
        }

        public async Task InsertDocument(Permissions permissions)
        {
            _logger.LogInformation("InsertDocument in Elastic");

            if (!_client.IndexExists(_elasticIndex).Exists)
            {
                _client.CreateIndex(_elasticIndex);
                _logger.LogInformation("CreateIndex in Elastic");
            }

            await _client.IndexAsync(permissions, i => i
               .Id(permissions.Id)
               .Index(_elasticIndex)
               .Type("permissions"));
        }
    }
}
