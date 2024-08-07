using System.Data.SqlClient;

namespace Onion.Cqrs.Application.Interface.Context
{
    public interface IDapperContext
    {
        public SqlConnection GetConnection();
        public void Execute(Action<SqlConnection> @event);
    }
}
