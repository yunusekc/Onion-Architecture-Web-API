using Dapper;
using Newtonsoft.Json;
using Onion.Cqrs.Application.DTO;
using Onion.Cqrs.Application.Interface;
using Onion.Cqrs.Application.Interface.Context;
using Onion.Cqrs.Application.SecurityExtensions;
using Onion.Cqrs.Domain;
using System.Data;

namespace Onion.Cqrs.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly IDapperContext dapperContext;
        private string tableName;

        public GenericRepository(IDapperContext dapperContext, string tablename)
        {
            this.dapperContext = dapperContext;
            this.tableName = tablename;
        }

        public async Task<T> AddAsync(T entity, string spName, DynamicParameters p)
        {
            using (var conn = dapperContext.GetConnection())
            {
                var rt = conn.QueryAsync<T>(spName, p, commandType: CommandType.StoredProcedure).Result;
                return rt.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(string spName)
        {
            using (var conn = dapperContext.GetConnection())
            {
                var rt = await conn.QueryAsync<T>(spName, commandType: System.Data.CommandType.StoredProcedure);
                #region API Key Security
                foreach (var customer in rt)
                {
                    if (!string.IsNullOrEmpty(customer.CustomerKey))
                    {
                        try
                        {
                            var customerKeyDTO = JsonConvert.DeserializeObject<CustomerKeyDTO>(customer.CustomerKey);
                            customer.APIKey = customerKeyDTO.APIKey.TextSifreCoz();
                            customer.APIPassword = customerKeyDTO.APIPassword.TextSifreCoz();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                #endregion
                return rt;
            }
        }

        public async Task<T> GetByIdAsync(Guid Id, string spName, DynamicParameters p)
        {
            using (var conn = dapperContext.GetConnection())
            {

                var rt = conn.QueryFirstAsync<T>(spName, p, commandType: CommandType.StoredProcedure).Result;
                #region API Key Resolving
                if (!string.IsNullOrEmpty(rt.CustomerKey))
                {
                    var customerKeyDTO = JsonConvert.DeserializeObject<CustomerKeyDTO>(rt.CustomerKey);
                    rt.APIKey = customerKeyDTO.APIKey.TextSifreCoz();
                    rt.APIPassword = customerKeyDTO.APIPassword.TextSifreCoz();
                }
                #endregion
                return rt;
            }
        }
        public async Task<T> Delete(Guid Id, string spName, DynamicParameters p)
        {
            using (var conn = dapperContext.GetConnection())
            {
                var rt = conn.QueryFirstAsync<T>(spName, p, commandType: CommandType.StoredProcedure).Result;
                var obj = JsonConvert.SerializeObject(rt);
                return rt;
            }
        }
        public async Task<T> Update(T entity, string spName, DynamicParameters p)
        {
            using (var conn = dapperContext.GetConnection())
            {
                var rt = conn.QueryAsync<Guid>(spName, p, commandType: CommandType.StoredProcedure).Result;
                entity.Id = rt.FirstOrDefault();
                return entity;
            }
        }
    }
}
