using Dapper;
using MediatR;
using Onion.Cqrs.Application.Interface;
using Onion.Cqrs.Application.Wrapper;

namespace Onion.Cqrs.Application.Features.Commands.DeleteCustomer
{
    public class DeleteCustomerCmdHandler : IRequestHandler<DeleteCustomerCmd, ServiceResponse<Guid>>
    {
        private readonly ICustomerRepository customerRepository;

        public DeleteCustomerCmdHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public async Task<ServiceResponse<Guid>> Handle(DeleteCustomerCmd request, CancellationToken cancellationToken)
        {
            try
            {
                #region Adding Related Parameters
                var p = new DynamicParameters();
                p.Add("@ID", request.Id);
                #endregion

                var rt = await customerRepository.Delete(request.Id, "SP_DELETE_CUSTOMER", p);
                return new ServiceResponse<Guid>(rt.Id) { IsSuccess = true, Message = "success", Value = request.Id};
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Guid>(request.Id)
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
