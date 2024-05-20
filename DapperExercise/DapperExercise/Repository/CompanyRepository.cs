using Dapper;
using DapperExercise.Contracts;
using DapperExercise.Context;
using DapperExercise.Entities;
using DapperExercise.DTO;
using System.Data;

namespace DapperExercise.Repository;

public class CompanyRepository : ICompanyRepository
{
    private readonly DapperContext _context;

    public CompanyRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Company>> GetCompanies()
    {
        // Using alias companyName
        const string query = "SELECT Id, Name AS CompanyName, Address, Country FROM Company";

        using var connection = _context.CreateConnection();
        var companies = await connection.QueryAsync<Company>(query);
        return companies.ToList();
    }

    public async Task<IEnumerable<Company>> GetCompanyById(int id)
    {
        const string query = "SELECT * FROM Company WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var company = await connection.QueryAsync<Company>(query, new { Id = id });
        return company;
    }

    public async Task<Company> CreateCompany(CompanyForCreationDTO company)
    {
        const string query = "INSERT INTO Company (Name, Address, Country) VALUES (@Name, @Address, @Country)" +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";

        var parameters = new DynamicParameters();
        parameters.Add("Name", company.Name, DbType.String);
        parameters.Add("Address", company.Address, DbType.String);
        parameters.Add("Country", company.Country, DbType.String);

        using var connection = _context.CreateConnection();
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

    public async Task UpdateCompany(int id, CompanyForUpdateDTO company)
    {
        const string query = "UPDATE Company SET Name = @Name, Address = @Address, Country = @Country WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32);
        parameters.Add("Name", company.Name, DbType.String);
        parameters.Add("Address", company.Address, DbType.String);
        parameters.Add("Country", company.Country, DbType.String);

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }

    public async Task DeleteCompany(int id)
    {
        const string query = "DELETE FROM Company WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32);

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }

    public async Task<Company> GetCompanyByEmployeeId(int id)
    {
        const string procedureName = "ShowCompanyForProvidedEmployeeId";
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        using var connection = _context.CreateConnection();
        var company = await connection.QueryFirstOrDefaultAsync<Company>
            (procedureName, parameters, commandType: CommandType.StoredProcedure);

        return company;
    }

    public async Task<Company> GetCompanyEmployeesMultipleResults(int id)
    {
        const string query = "SELECT * FROM Company WHERE Id = @Id; SELECT * FROM Employee WHERE CompanyId = @Id";

        using var connection = _context.CreateConnection();
        await using var multi = await connection.QueryMultipleAsync(query, new { Id = id });

        var company = multi.ReadFirstOrDefault<Company>();
        var employees = multi.Read<Employee>();

        company.Employees = employees.ToList();
        return company;
    }

    public async Task<List<Company>> GetCompanyEmployeesMultipleMapping()
    {
        const string query = "SELECT * FROM Company c JOIN Employee e ON c.Id = e.CompanyId";
        using var connection = _context.CreateConnection();
        var companyDict = new Dictionary<int, Company>();
        var companies = await connection.QueryAsync<Company, Employee, Company>(
            query, (company, employee) =>
            {
                if (!companyDict.TryGetValue(company.Id, out var currentCompany))
                {
                    currentCompany = company;
                    companyDict.Add(currentCompany.Id, currentCompany);
                }

                currentCompany.Employees.Add(employee);
                return currentCompany;
            }
        );
        return companies.Distinct().ToList();
    }
}