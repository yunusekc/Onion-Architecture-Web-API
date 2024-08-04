using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Cqrs.Application.Interface.Context
{
    public interface IDapperContext
    {
        public SqlConnection GetConnection();
        public void Execute(Action<SqlConnection> @event);
    }
}
