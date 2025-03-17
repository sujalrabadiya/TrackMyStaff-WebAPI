using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackMyStaffWebApplication.Data;
using TrackMyStaffWebApplication.Models;

namespace TrackMyStaffWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthRepository _authRepository;

        public AuthController(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        #region Add Company
        [HttpPost]
        public IActionResult Add([FromBody] RegisterationModel registerationModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isAdded = _authRepository.Insert(registerationModel);
            if (isAdded)
                return Ok(new { Message = "Registered successfully." });

            return StatusCode(500, new { Message = "An error occurred while Registering." });
        }
        #endregion

        #region Register Company
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            var user = _authRepository.Login(loginModel.Phone, loginModel.Password);
            if (user == null)
                return Unauthorized(new { Message = "Invalid credentials." });

            return Ok(user);
        }
        #endregion
    }
}
