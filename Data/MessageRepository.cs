using Microsoft.Data.SqlClient;
using System.Data;
using TrackMyStaffWebApplication.Models;

namespace TrackMyStaffWebApplication.Data
{
    public class MessageRepository
    {
        private readonly string _configuration;

        #region Constructor
        public MessageRepository(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region Send Message
        public bool SendMessage(MessageModel message)
        {
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_Message_Send";
                cmd.Parameters.AddWithValue("@SenderId", message.SenderId);
                cmd.Parameters.AddWithValue("@ReceiverId", message.ReceiverId);
                cmd.Parameters.AddWithValue("@Message", message.Message);
                cmd.Parameters.AddWithValue("@ImageURL", message.ImageUrl);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Get Conversation
        public IEnumerable<MessageModel> GetConversation(int userId1, int userId2)
        {
            var messages = new List<MessageModel>();
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_Message_GetConversation";
                cmd.Parameters.AddWithValue("@UserId1", userId1);
                cmd.Parameters.AddWithValue("@UserId2", userId2);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    messages.Add(new MessageModel
                    {
                        SenderId = Convert.ToInt32(reader["sender_id"]),
                        ReceiverId = Convert.ToInt32(reader["receiver_id"]),
                        Message = reader["message"].ToString(),
                        ImageUrl = reader["image_url"].ToString(),
                        SentAt = Convert.ToDateTime(reader["sent_at"])
                    });
                }
            }
            return messages;
        }
        #endregion

        #region Clear Chat
        public bool ClearChat(int userId1, int userId2)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PR_Message_ClearChat";
                    cmd.Parameters.AddWithValue("@UserId1", userId1);
                    cmd.Parameters.AddWithValue("@UserId2", userId2);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region Get Conversation List
        public IEnumerable<ConversationListModel> GetConversationList(int userId)
        {
            try
            {
                var conversations = new List<ConversationListModel>();
                using (SqlConnection connection = new SqlConnection(_configuration))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PR_Message_GetConversationUsers";
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        conversations.Add(new ConversationListModel
                        {
                            OtherUserId = reader["other_user_id"] != DBNull.Value ? Convert.ToInt32(reader["other_user_id"]) : (int?)null,
                            OtherUserName = reader["other_user_name"]?.ToString(),
                            OtherUserPhone = reader["other_user_phone"]?.ToString(),
                            OtherUserImgUrl = reader["other_user_img_url"]?.ToString(),
                            Message = reader["message"]?.ToString(),
                            MessageUrl = reader["image_url"]?.ToString(),
                            SentAt = reader["sent_at"] != DBNull.Value ? Convert.ToDateTime(reader["sent_at"]) : (DateTime?)null
                        });
                    }
                }
                return conversations;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion
    }
}
