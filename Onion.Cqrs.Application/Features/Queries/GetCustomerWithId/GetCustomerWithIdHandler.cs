using Dapper;
using MediatR;
using Onion.Cqrs.Application.DTO;
using Onion.Cqrs.Application.Interface;
using Onion.Cqrs.Application.Wrapper;

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
            #region Adding Related Parameters
            var p = new DynamicParameters();
            p.Add("@ID", request.Id);
            #endregion

            var rt = await customerRepository.GetByIdAsync(request.Id, "SP_GET_CUSTUMER_WITH_ID", p);
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
