using MediatR;
using Onion.Cqrs.Application.Wrapper;

namespace Onion.Cqrs.Application.Features.Commands.DeleteCustomer
{
    public class DeleteCustomerCmd : IRequest<ServiceResponse<Guid>>
    {
        public Guid Id { get; set; }
    }
}
