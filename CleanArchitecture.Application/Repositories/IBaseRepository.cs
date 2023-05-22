using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    void Create(T entity);
    void Update(T entity);
    Task<List<T>> GetAll(CancellationToken cancellationToken);
}