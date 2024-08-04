using AutoMapper.Execution;
using MediatR;
using Newtonsoft.Json;
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

namespace Onion.Cqrs.Application.Features.Commands.CreateProduct
{
    public class CreateCustomerCmdHandler : IRequestHandler<CreateCustomerCmd, ServiceResponse<CustomerEntity>>
    {
        private readonly ICustomerRepository customerRepository;

        public CreateCustomerCmdHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<ServiceResponse<CustomerEntity>> Handle(CreateCustomerCmd request, CancellationToken cancellationToken)
        {
            try
            {
                var encryptedAPIKey = request.CustomerKeys.APIKey.TextSifrele();
                var encryptedAPIPassword = request.CustomerKeys.APIPassword.TextSifrele();

                var customerKeyJson = JsonConvert.SerializeObject(new
                {
                    APIKey = encryptedAPIKey,
                    APIPassword = encryptedAPIPassword
                });

                var entity = new CustomerEntity()
                {
                    Id = Guid.NewGuid(),
                    CustomerKey = customerKeyJson,
                    Name = request.Name,
                    Surname = request.Surname
                };
                var rt = await customerRepository.AddAsync(entity);
                return new ServiceResponse<CustomerEntity>(rt)
                {
                    IsSuccess = true,
                    Message = "Success",
                    Value = entity
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<CustomerEntity>(new CustomerEntity())
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Value = null
                    
                };
            }
        }
    }
}
