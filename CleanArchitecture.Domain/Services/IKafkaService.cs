namespace CleanArchitecture.Domain.Services
{
    public interface IKafkaService
    {
        Task ProduceMessageAsync(string operation);
    }
}
