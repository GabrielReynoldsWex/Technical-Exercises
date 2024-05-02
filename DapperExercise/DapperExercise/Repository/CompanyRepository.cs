using Dapper;
using DapperExercise.Contracts;
using DapperExercise.Context;
using DapperExercise.Entities;

namespace DapperExercise.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _Context;

        public CompanyRepository(DapperContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var query = "SELECT * FROM Company";

            using (var connection = _Context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }
        }
    }
}
