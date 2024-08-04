using MediatR;
using Newtonsoft.Json;
using Onion.Cqrs.Application.DTO;
using Onion.Cqrs.Application.Interface;
using Onion.Cqrs.Application.SecurityExtensions;
using Onion.Cqrs.Domain;
using System.Text.Json.Nodes;

namespace Onion.Cqrs.Application.Features.Queries.GetAllCustomers
{
    public class GetlAllCustomersQuery : IRequest<List<CustomerViewDTO>>
    {
        public class GetAllCustomersQueryHandler : IRequestHandler<GetlAllCustomersQuery, List<CustomerViewDTO>>
        {
            private ICustomerRepository _customerRepository;
            public GetAllCustomersQueryHandler(ICustomerRepository customerInterface)
            {
                _customerRepository = customerInterface;
            }
            public async Task<List<CustomerViewDTO>> Handle(GetlAllCustomersQuery request, CancellationToken cancellationToken)
            {
                var customers = await _customerRepository.GetAllAsync();

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
                }).ToList();

                return customerViewDTOs;

                //return customers.Select(c => new CustomerViewDTO
                //{
                //    Id = c.Id,
                //    Name = c.Name,
                //    Surname = c.Surname
                //}).ToList();
            }
        }
    }
}
