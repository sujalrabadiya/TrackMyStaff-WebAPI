using Microsoft.AspNetCore.Mvc;
using TrackMyStaffWebApplication.Data;
using TrackMyStaffWebApplication.Models;

namespace TrackMyStaffWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region Get All Users
        [HttpGet("CompanyId/{companyId}")]
        public IActionResult GetAllUsers(int companyId)
        {
            var users = _userRepository.GetAllUsers(companyId);
            return Ok(users);
        }
        #endregion

        #region Get Users By Role
        [HttpGet("{companyId}/{role}")]
        public IActionResult GetUsersByRole(int companyId, string role)
        {
            var users = _userRepository.GetUsersByRole(companyId, role);
            if (users == null)
                return NotFound(new { Message = "User not found." });

            return Ok(users);
        }
        #endregion

        #region Get User By ID
        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            return Ok(user);
        }
        #endregion

        #region Add New User
        [HttpPost]
        public IActionResult AddUser([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var isAdded = _userRepository.InsertUser(user);
            if (isAdded)
                return Ok(new { Message = "User added successfully." });

            return StatusCode(500, new { Message = "An error occurred while adding the user." });
        }
        #endregion

        #region Update User
        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isUpdated = _userRepository.UpdateUser(user);
            if (isUpdated)
                return Ok(new { Message = "User updated successfully." });

            return NotFound(new { Message = "User not found or update failed." });
        }
        #endregion

        #region Delete User
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var isDeleted = _userRepository.DeleteUser(id);
            if (isDeleted)
                return Ok(new { Message = "User deleted successfully." });

            return NotFound(new { Message = "User not found or deletion failed." });
        }
        #endregion
    }
}
