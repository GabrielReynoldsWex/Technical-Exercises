using Dapper;
using DapperExercise.Context;

namespace DapperExercise.Migrations
{
    public class Database
    {
        private readonly DapperContext _dapperContext;
        public Database(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public void CreateDatabase(string dbName)
        {
            var query = "SELECT * FROM sys.databases WHERE name = @dbName";
            var parameters = new DynamicParameters();
            parameters.Add("@dbName", dbName);

            using (var connection = _dapperContext.CreateMasterConnection())
            {
                var records = connection.Query(query, parameters);
                if (!records.Any())
                {
                    query = $"CREATE DATABASE {dbName}";
                    connection.Execute(query);
                }  
            }
        }
    }
}
