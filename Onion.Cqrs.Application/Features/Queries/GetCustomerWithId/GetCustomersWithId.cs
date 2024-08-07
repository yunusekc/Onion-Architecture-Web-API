using MediatR;
using Onion.Cqrs.Application.DTO;
using Onion.Cqrs.Application.Wrapper;

namespace Onion.Cqrs.Application.Features.Queries.GetCustomerWithId
{
    public class GetCustomerWithId : IRequest<ServiceResponse<CustomerViewDTO>>
    {
        public Guid Id { get; set; }
        
    }
}
