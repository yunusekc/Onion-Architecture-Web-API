using Dapper;
using Newtonsoft.Json;
using Onion.Cqrs.Application.DTO;
using Onion.Cqrs.Application.Interface;
using Onion.Cqrs.Application.Interface.Context;
using Onion.Cqrs.Domain;
using Onion.Cqrs.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Cqrs.Persistence.Repositories
{
    public class CustomerRepository : GenericRepository<CustomerEntity>, ICustomerRepository
    {
        private readonly IDapperContext dapperContext;

        public CustomerRepository(IDapperContext dapperContext) : base(dapperContext, "CUSTOMERS")
        {
            this.dapperContext = dapperContext;
        }

        public async Task<IEnumerable<CustomerViewDTO>> GetAllCustomersAsync()
        {
            using (var conn = dapperContext.GetConnection())
            {
                conn.Open();
                var rt = await conn.QueryAsync<CustomerViewDTO>("SP_GET_CUSTOMERS", commandType: System.Data.CommandType.StoredProcedure);
                return rt;
            }
        }
    }
}
