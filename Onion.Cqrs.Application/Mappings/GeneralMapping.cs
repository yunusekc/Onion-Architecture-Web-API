using AutoMapper;
using Onion.Cqrs.Application.DTO;
using Onion.Cqrs.Domain;

namespace Onion.Cqrs.Application.Mappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<CustomerViewDTO, CustomerEntity>();
        }
    }
}
