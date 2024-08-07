using MediatR;
using Onion.Cqrs.Application.DTO;
using Onion.Cqrs.Application.Interface;

namespace Onion.Cqrs.Application.Features.Queries.GetAllCustomers
{
    public class GetlAllCustomersQuery : IRequest<IEnumerable<CustomerViewDTO>>
    {
        public class GetAllCustomersQueryHandler : IRequestHandler<GetlAllCustomersQuery, IEnumerable<CustomerViewDTO>>
        {
            private ICustomerRepository _customerRepository;
            public GetAllCustomersQueryHandler(ICustomerRepository customerInterface)
            {
                _customerRepository = customerInterface;
            }
            public async Task<IEnumerable<CustomerViewDTO>> Handle(GetlAllCustomersQuery request, CancellationToken cancellationToken)
            {
                var customers = await _customerRepository.GetAllAsync("SP_GET_CUSTOMERS");

                var customerViewDTOs = customers.Select(customer => new CustomerViewDTO
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Surname = customer.Surname,
                    CustomerKeyDTO = new CustomerKeyDTO
                    {
                        APIKey = customer.APIKey,
                        APIPassword = customer.APIPassword
                    }
                });

                return customerViewDTOs;
            }
        }
    }
}
