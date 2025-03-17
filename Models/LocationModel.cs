using System.ComponentModel.DataAnnotations.Schema;

namespace TrackMyStaffWebApplication.Models
{
    public class LocationModel
    {
        public int? Id { get; set; }

        [ForeignKey("UserModel")]
        public int UserId { get; set; }
        public string? UserName { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
