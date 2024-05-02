using DapperExercise.DTO;
using DapperExercise.Entities;

namespace DapperExercise.Contracts
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<IEnumerable<Company>> GetCompaniesByCountry(string country);
        public Task<Company> CreateCompany(CompanyForCreationDTO company);


    }
}
