using Onion.Cqrs.Domain;

namespace Onion.Cqrs.Application.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid Id);
        Task<T> AddAsync(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(Guid Id);
    }
}
