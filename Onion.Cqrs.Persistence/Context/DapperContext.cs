﻿using Microsoft.Extensions.Configuration;
using Onion.Cqrs.Application.Interface.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Cqrs.Persistence.Context
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration configuration;

        public DapperContext(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public void Execute(Action<SqlConnection> @event)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                @event(connection);
            }
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
