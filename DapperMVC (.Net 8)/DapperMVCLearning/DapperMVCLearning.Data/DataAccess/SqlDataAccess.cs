using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperMVCLearning.Data.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;
        //private readonly IDbConnection _connection;

        //constructor
        public SqlDataAccess(IConfiguration config) // IDbConnection connection
        {
            this._config = config;
            //connection = connection;
        }


        // T is return type, and P is parameter type
        // spname= stored procedure name
        public async Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters, string connectionId = "SqlServerConnection")
        {
            //using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            using IDbConnection connection = new SqlConnection(_config["ConnectionStrings:"+connectionId]);
            return await connection.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
        }

        // T is return type and parameter type
        // spname= stored procedure name
        public async Task SaveData<T>(string spName, T parameters, string connectionId = "SqlServerConnection")
        {
            //using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            using IDbConnection connection = new SqlConnection(_config["ConnectionStrings:" + connectionId]);
            await connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
