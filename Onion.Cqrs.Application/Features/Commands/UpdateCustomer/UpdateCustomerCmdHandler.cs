using Dapper;
using MediatR;
using Newtonsoft.Json;
using Onion.Cqrs.Application.Interface;
using Onion.Cqrs.Application.SecurityExtensions;
using Onion.Cqrs.Application.Wrapper;
using Onion.Cqrs.Domain;

namespace Onion.Cqrs.Application.Features.Commands.UpdateCustomer
{
    internal class UpdateCustomerCmdHandler : IRequestHandler<UpdateCustomerCmd, ServiceResponse<CustomerEntity>>
    {
        private readonly ICustomerRepository customerRepository;

        public UpdateCustomerCmdHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public async Task<ServiceResponse<CustomerEntity>> Handle(UpdateCustomerCmd request, CancellationToken cancellationToken)
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
                    Id = request.Id,
                    CustomerKey = customerKeyJson,
                    Name = request.Name,
                    Surname = request.Surname
                };

                #region  Adding Related Parameters
                var p = new DynamicParameters();
                p.Add("@ID", entity.Id);
                p.Add("@CUSTOMERKEY", entity.CustomerKey);
                p.Add("@NAME", entity.Name);
                p.Add("@SURNAME", entity.Surname);

                #endregion
                var rt = await customerRepository.Update(entity, "SP_UPDATE_CUSTOMER", p);
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
