using Onion.Cqrs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Cqrs.Application.DTO
{
    public class CustomerViewDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public CustomerKeyDTO? CustomerKeyDTO { get; set; }
    }
}
