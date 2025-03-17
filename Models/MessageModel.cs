using System.ComponentModel.DataAnnotations.Schema;

namespace TrackMyStaffWebApplication.Models
{
    public class MessageModel
    {
        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string? Message { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime? SentAt { get; set; } = DateTime.Now;
    }

    public class ConversationListModel
    {
        public int? OtherUserId { get; set; }
        
        public string? OtherUserName { get; set; }

        public string? OtherUserPhone { get; set; }

        public string? OtherUserImgUrl { get; set; }

        public string? Message { get; set; }
        
        public string? MessageUrl { get; set; }

        public DateTime? SentAt { get; set; }
    }
}
