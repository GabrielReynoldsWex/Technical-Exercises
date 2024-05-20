using DapperExercise.Contracts;
using DapperExercise.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        [HttpGet("GetCompanies")]
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

        [HttpGet("{id}", Name = "CompanyById")]
        public async Task<IActionResult> GetCompaniesByCountry(int id)
        {
            try
            {
                var company = await _companyRepo.GetCompanyById(id);
                if (company.IsNullOrEmpty())
                    return NotFound();
                return Ok(company);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("CreateCompany")]
        public async Task<IActionResult> CreateCompany(CompanyForCreationDTO company)
        {
            try
            {
                var createdCompany = await _companyRepo.CreateCompany(company);
                
                // This implementation doesn't make much sense for now. Might fix later - works for the exercise though
                return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}", Name = "UpdateCompany")]
        public async Task<IActionResult> UpdateCompany(int id, CompanyForUpdateDTO company)
        {
            try
            {
                var dbCompany = await _companyRepo.GetCompanyById(id);
                if (dbCompany.IsNullOrEmpty())
                    return NotFound();
                await _companyRepo.UpdateCompany(id, company);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}", Name = "DeleteCompany")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                var dbCompany = await _companyRepo.GetCompanyById(id);
                if (dbCompany.IsNullOrEmpty())
                    return NotFound();
                await _companyRepo.DeleteCompany(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetCompanyByEmployeeId/{id}")]
        public async Task<IActionResult> GetCompanyByEmployeeId(int id)
        {
            try
            {
                var company = await _companyRepo.GetCompanyByEmployeeId(id);
                if (company.Equals(null))
                    return NotFound();
                return Ok(company);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetEmployeesByCompanyId/{id}")]
        public async Task<IActionResult> GetCompanyEmployeesMultipleResult(int id)
        {
            try
            {
                var company = await _companyRepo.GetCompanyEmployeesMultipleResults(id);
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

        [HttpGet("MultipleMapping")]
        public async Task<IActionResult> GetCompaniesEmployeesMultipleMapping()
        {
            try
            {
                var company = await _companyRepo.GetCompanyEmployeesMultipleMapping();
                return Ok(company);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}