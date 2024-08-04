using MediatR;
using Onion.Cqrs.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Cqrs.Application.Features.Commands.DeleteCustomer
{
    public class DeleteCustomerCmd : IRequest<ServiceResponse<Guid>>
    {
        public Guid Id { get; set; }
    }
}
