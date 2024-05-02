using DapperExercise.Contracts;
using DapperExercise.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DapperExercise.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepo;

        public CompaniesController(ICompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await _companyRepo.GetCompanies();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{country}", Name = "CompaniesByCountry")]
        public async Task<IActionResult> GetCompaniesByCountry(string country)
        {
            try
            {
                var company = await _companyRepo.GetCompaniesByCountry(country);
                if (company == null)
                    return NotFound();
                return Ok(company);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyForCreationDTO company)
        {
            try
            {
                var createdCompany = await _companyRepo.CreateCompany(company);

                // This implementation doesn't make much sense for now. Might fix later - works for the exercise though
                return CreatedAtRoute("CompaniesByCountry", new { country = createdCompany.Country }, createdCompany);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}