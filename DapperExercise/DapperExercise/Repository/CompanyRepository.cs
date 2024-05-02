using Dapper;
using DapperExercise.Contracts;
using DapperExercise.Context;
using DapperExercise.Entities;
using DapperExercise.DTO;
using System.Data;

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
            // Using alias companyName
            var query = "SELECT Id, Name AS CompanyName, Address, Country FROM Company";

            using (var connection = _Context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }
        }

        public async Task<IEnumerable<Company>> GetCompaniesByCountry(string country)
        {
            var query = "SELECT * FROM Company WHERE Country = @country";

            using (var connection = _Context.CreateConnection())
            {
                var company = await connection.QueryAsync<Company>(query, new { Country = country });
                return company;
            }
        }
        public async Task<Company> CreateCompany(CompanyForCreationDTO company)
        {
            var query = "INSERT INTO Company (Name, Address, Country) VALUES (@Name, @Address, @Country)" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)";
            var parameters = new DynamicParameters();

            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);

            using (var connection = _Context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdCompany = new Company
                {
                    Id = id,
                    Name = company.Name,
                    Address = company.Address,
                    Country = company.Country
                };
                return createdCompany;
            }
        }
    }
}
