using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackMyStaffWebApplication.Data;
using TrackMyStaffWebApplication.Models;

namespace TrackMyStaffWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly LocationRepository _locationRepository;

        public LocationController(LocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        #region Add Location
        [HttpPost("add")]
        public async Task<IActionResult> AddLocationAsync([FromBody] LocationModel locationModel)
        {
            try
            {
                var isAdded = _locationRepository.AddLocation(locationModel);
                if (isAdded)
                {
                    locationModel.UserName = _locationRepository.GetUserNameById(locationModel.UserId);
                    return Ok(new { Message = "Location added successfully." });
                }
                return StatusCode(500, new { Message = "Failed to insert location." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { Message = "Server error", Error = ex.Message });
            }
        }
        #endregion

        //#region Get Live Locations By Staff Id
        //[HttpGet("live/{userId}")]
        //public IActionResult GetLiveLocationsByCompany(int userId)
        //{
        //    var location = _locationRepository.GetLocation(userId);
        //    return Ok(location);
        //}
        //#endregion

        #region Get Live Locations By Company
        [HttpGet("live/{companyId}")]
        public IActionResult GetLiveLocationsByCompany(int companyId)
        {
            var locations = _locationRepository.GetLiveLocationsByCompany(companyId);
            return Ok(locations);
        }
        #endregion

        //# region Get Location History
        //[HttpGet("history/staff/{userId}")]
        //public IActionResult GetLocationHistory(int userId, DateTime startDate, DateTime endDate)
        //{
        //    var history = _locationRepository.GetLocationHistory(userId, startDate, endDate);
        //    return Ok(history);
        //}
        //#endregion
    }
}
