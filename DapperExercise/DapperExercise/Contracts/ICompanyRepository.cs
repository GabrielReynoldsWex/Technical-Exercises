using DapperExercise.DTO;
using DapperExercise.Entities;

namespace DapperExercise.Contracts
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<IEnumerable<Company>> GetCompanyById(int id);
        public Task<Company> CreateCompany(CompanyForCreationDTO company);
        public Task UpdateCompany(int id, CompanyForUpdateDTO company);
        public Task DeleteCompany(int id);
    }
}
