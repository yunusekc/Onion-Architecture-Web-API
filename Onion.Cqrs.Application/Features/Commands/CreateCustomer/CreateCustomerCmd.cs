using MediatR;
using Onion.Cqrs.Application.DTO;
using Onion.Cqrs.Application.Wrapper;
using Onion.Cqrs.Domain;

namespace Onion.Cqrs.Application.Features.Commands.CreateProduct
{
    public class CreateCustomerCmd: IRequest<ServiceResponse<CustomerEntity>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public CustomerKeyDTO CustomerKeys { get; set; }
    }
}
