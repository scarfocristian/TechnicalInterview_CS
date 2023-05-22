using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Services
{
    public interface IElasticSearchService
    {
        Task InsertDocument(Permissions permissions);
    }
}
