using MediatR;
using Onion.Cqrs.Application.Interface;
using Onion.Cqrs.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var rt = await customerRepository.Delete(request.Id);
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
