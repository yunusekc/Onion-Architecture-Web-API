using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Cqrs.Domain
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CustomerKey { get; set; }
        public string? APIKey { get; set; }
        public string? APIPassword { get; set; }
    }
}
