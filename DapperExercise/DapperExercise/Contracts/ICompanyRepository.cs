using DapperExercise.Entities;

namespace DapperExercise.Contracts
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();

    }
}
