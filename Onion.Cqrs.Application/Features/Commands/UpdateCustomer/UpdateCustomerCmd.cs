using MediatR;
using Onion.Cqrs.Application.DTO;
using Onion.Cqrs.Application.Wrapper;
using Onion.Cqrs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Cqrs.Application.Features.Commands.UpdateCustomer
{
    public class UpdateCustomerCmd : IRequest<ServiceResponse<CustomerEntity>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public CustomerKeyDTO CustomerKeys { get; set; }
    }
}
