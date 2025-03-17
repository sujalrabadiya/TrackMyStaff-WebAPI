using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackMyStaffWebApplication.Data;
using TrackMyStaffWebApplication.Models;

namespace TrackMyStaffWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyRepository _companyRepository;

        public CompanyController(CompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        #region Get All Companies
        [HttpGet]
        public IActionResult GetAll()
        {
            var companies = _companyRepository.GetAll();
            return Ok(companies);
        }
        #endregion

        #region Get Company By ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var company = _companyRepository.GetById(id);
            if (company == null)
                return NotFound(new { Message = "Company not found." });

            return Ok(company);
        }
        #endregion

        #region Update Company
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CompanyModel company)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            company.id = id;
            var isUpdated = _companyRepository.Update(company);
            if (isUpdated)
                return Ok(new { Message = "Company updated successfully." });

            return NotFound(new { Message = "Company not found or update failed." });
        }
        #endregion

        #region Delete Company
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _companyRepository.Delete(id);
            if (isDeleted)
                return Ok(new { Message = "Company deleted successfully." });

            return NotFound(new { Message = "Company not found or deletion failed." });
        }
        #endregion
    }
}
