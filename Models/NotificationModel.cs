using System.ComponentModel.DataAnnotations.Schema;

namespace TrackMyStaffWebApplication.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }

        [ForeignKey("UserModel")]
        public int UserId { get; set; }

        public string Title { get; set; }

        public string? Body { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
