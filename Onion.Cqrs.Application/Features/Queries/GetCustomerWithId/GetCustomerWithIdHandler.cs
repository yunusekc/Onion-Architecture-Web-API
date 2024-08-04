using MediatR;
using Onion.Cqrs.Application.DTO;
using Onion.Cqrs.Application.Interface;
using Onion.Cqrs.Application.SecurityExtensions;
using Onion.Cqrs.Application.Wrapper;
using Onion.Cqrs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Cqrs.Application.Features.Queries.GetCustomerWithId
{
    public class GetCustomerWithIdHandler : IRequestHandler<GetCustomerWithId, ServiceResponse<CustomerViewDTO>>
    {
        private readonly ICustomerRepository customerRepository;

        public GetCustomerWithIdHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public async Task<ServiceResponse<CustomerViewDTO>> Handle(GetCustomerWithId request, CancellationToken cancellationToken)
        {
            var rt = await customerRepository.GetByIdAsync(request.Id);
            var customerViewDTOs = new CustomerViewDTO
            {
                Id = rt.Id,
                Name = rt.Name,
                Surname = rt.Surname,
                CustomerKeyDTO = new CustomerKeyDTO
                {
                    APIKey = rt.APIKey,
                    APIPassword = rt.APIPassword
                }
            };
            return new ServiceResponse<CustomerViewDTO>(customerViewDTOs);
        }
    }
}
