using MediatR;
using Onion.Cqrs.Application.DTO;
using Onion.Cqrs.Application.Interface;
using Onion.Cqrs.Application.Wrapper;
using Onion.Cqrs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Cqrs.Application.Features.Queries.GetCustomerWithId
{
    public class GetCustomerWithId : IRequest<ServiceResponse<CustomerViewDTO>>
    {
        public Guid Id { get; set; }
        
    }
}
