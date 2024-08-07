using Onion.Cqrs.Application.DTO;
using Onion.Cqrs.Domain;

namespace Onion.Cqrs.Application.Interface
{
    public interface ICustomerRepository : IGenericRepository<CustomerEntity>
    {
        Task<IEnumerable<CustomerViewDTO>> GetAllCustomersAsync();
    }
}
