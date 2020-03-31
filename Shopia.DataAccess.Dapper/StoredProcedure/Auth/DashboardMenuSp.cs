using System;
using Elk.Dapper;
using Shopia.Domain;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Shopia.DataAccess.Dapper
{
    public class DashboardMenuSp
    {
        private SqlConnection _sqlConnection;

        public DashboardMenuSp(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }

        public IEnumerable<MenuSPModel> GetUserMenu(Guid userId)
            => _sqlConnection.ExecuteSpList<MenuSPModel>("[Auth].[GetUserMenu]", new { UserId = userId });
    }
}