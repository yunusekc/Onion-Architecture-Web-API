using Onion.Cqrs.Application.DTO;
using Onion.Cqrs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Cqrs.Application.Interface
{
    public interface ICustomerRepository : IGenericRepository<CustomerEntity>
    {
        Task<IEnumerable<CustomerViewDTO>> GetAllCustomersAsync();
    }
}
