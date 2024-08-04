using Dapper;
using Newtonsoft.Json;
using Onion.Cqrs.Application.DTO;
using Onion.Cqrs.Application.Interface;
using Onion.Cqrs.Application.Interface.Context;
using Onion.Cqrs.Application.SecurityExtensions;
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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly IDapperContext dapperContext;
        private string tableName;

        public GenericRepository(IDapperContext dapperContext, string tablename )
        {
            this.dapperContext = dapperContext;
            this.tableName = tablename;
        }

        public async Task<T> AddAsync(T entity)
        {
            using (var conn = dapperContext.GetConnection())
            {
                conn.Open();
                var p = new DynamicParameters();
                p.Add("@ID", value: entity.Id, dbType: DbType.Guid);
                p.Add("@Name", value: entity.Name);
                p.Add("@Surname", value: entity.Surname);
                p.Add("@CustomerKey", value: entity.CustomerKey);
                var rt = conn.QueryAsync<T>("SP_ADD_CUSTOMER", p, commandType: CommandType.StoredProcedure).Result;
                return rt.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //var query = $"SELECT * FROM {tableName}";
            using (var conn = dapperContext.GetConnection())
            {
                conn.Open();
                //var rt = await conn.QueryAsync<T>(query);
                //return rt.FirstOrDefault();
                var rt = await conn.QueryAsync<T>("SP_GET_CUSTOMERS", commandType: System.Data.CommandType.StoredProcedure);
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

        public async Task<T> GetByIdAsync(Guid Id)
        {
            using (var conn = dapperContext.GetConnection())
            {
                conn.Open();
                var p = new DynamicParameters();
                p.Add("@ID", value: Id, dbType: DbType.Guid);
                var rt = conn.QueryFirstAsync<T>("SP_GET_CUSTOMER_WITH_ID", p, commandType: CommandType.StoredProcedure).Result;
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
        public async Task<T> Delete(Guid Id)
        {
            using (var conn = dapperContext.GetConnection())
            {
                conn.Open();
                var p = new DynamicParameters();
                p.Add("@ID", value: Id, dbType: DbType.Guid);
                var rt = conn.QueryFirstAsync<T>("SP_DELETE_CUSTOMER", p, commandType: CommandType.StoredProcedure).Result;
                var obj = JsonConvert.SerializeObject(rt);
                return rt;
            }
        }
        public async Task<T> Update(T entity)
        {
            using (var conn = dapperContext.GetConnection())
            {
                conn.Open();
                var p = new DynamicParameters();
                p.Add("@ID", value: entity.Id, dbType: DbType.Guid);
                p.Add("@Name", value: entity.Name);
                p.Add("@Surname", value: entity.Surname);
                p.Add("@CustomerKey", value: entity.CustomerKey);
                var rt = conn.QueryAsync<Guid>("SP_UPDATE_CUSTOMER", p, commandType: CommandType.StoredProcedure).Result;
                entity.Id = rt.FirstOrDefault();
                return entity;
            }
        }
    }
}
