using Dapper;
using Onion.Cqrs.Domain;

namespace Onion.Cqrs.Application.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(string spName);
        Task<T> GetByIdAsync(Guid Id, string spName, DynamicParameters p);
        Task<T> AddAsync(T entity, string spName, DynamicParameters p);
        Task<T> Update(T entity, string spName, DynamicParameters p);
        Task<T> Delete(Guid Id, string spName, DynamicParameters p);
    }
}
