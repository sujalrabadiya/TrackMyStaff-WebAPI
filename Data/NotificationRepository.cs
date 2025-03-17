using Microsoft.Data.SqlClient;
using System.Data;
using TrackMyStaffWebApplication.Models;

namespace TrackMyStaffWebApplication.Data
{
    public class NotificationRepository
    {
        private readonly string _configuration;

        #region Constructor
        public NotificationRepository(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region Send Notification
        public bool SendNotification(NotificationModel notification)
        {
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_Notification_Send";
                cmd.Parameters.AddWithValue("@UserId", notification.UserId);
                cmd.Parameters.AddWithValue("@Title", notification.Title);
                cmd.Parameters.AddWithValue("@Body", notification.Body);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Get Notifications
        public IEnumerable<NotificationModel> GetNotifications(int userId)
        {
            var notifications = new List<NotificationModel>();
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_Notification_Get";
                cmd.Parameters.AddWithValue("@UserId", userId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    notifications.Add(new NotificationModel
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Title = reader["title"].ToString(),
                        Body = reader["body"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["created_at"])
                    });
                }
            }
            return notifications;
        }
        #endregion
    }
}
