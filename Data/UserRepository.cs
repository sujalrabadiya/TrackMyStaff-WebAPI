using Microsoft.Data.SqlClient;
using System.Data;
using TrackMyStaffWebApplication.Models;

namespace TrackMyStaffWebApplication.Data
{
    public class UserRepository
    {
        #region Configuration
        private readonly string _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region Select All Users
        public IEnumerable<UserModel> GetAllUsers(int companyId)
        {
            var users = new List<UserModel>();
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("PR_User_SelectAll", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CompanyId", companyId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new UserModel
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Email = reader["email"].ToString(),
                        Name = reader["name"].ToString(),
                        Phone = reader["phone"].ToString(),
                        ImgUrl = reader["img_url"].ToString(),
                        Role = reader["role"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["created_at"]),
                        UpdatedAt = Convert.ToDateTime(reader["updated_at"])
                    });
                }
            }
            return users;
        }
        #endregion

        #region Get User by ID
        public UserModel GetUserById(int userId)
        {
            UserModel user = null;
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("PR_User_SelectByPK", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserId", userId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new UserModel
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Email = reader["email"].ToString(),
                        Name = reader["name"].ToString(),
                        Phone = reader["phone"].ToString(),
                        Role = reader["role"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["created_at"]),
                        UpdatedAt = Convert.ToDateTime(reader["updated_at"])
                    };
                }
            }
            return user;
        }
        #endregion

        #region Get Users by Role
        public IEnumerable<UserModel> GetUsersByRole(int companyId, string role)
        {
            var users = new List<UserModel>();
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("PR_User_SelectByRole", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CompanyId", companyId);
                cmd.Parameters.AddWithValue("@Role", role);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new UserModel
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Email = reader["email"].ToString(),
                        Name = reader["name"].ToString(),
                        Phone = reader["phone"].ToString(),
                        Role = reader["role"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["created_at"]),
                        UpdatedAt = Convert.ToDateTime(reader["updated_at"])
                    });
                }
            }
            return users;
        }
        #endregion

        #region Insert User
        public bool InsertUser(UserModel user)
        {
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("PR_Staff_AddByCompanyId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@ImgUrl", user.ImgUrl);
                cmd.Parameters.AddWithValue("@CompanyId", user.CompanyId);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update User
        public bool UpdateUser(UserModel user)
        {
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("PR_User_UpdateByPK", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", user.Id);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@Role", user.Role);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Delete User
        public bool DeleteUser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("PR_User_DeleteByPK", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserId", userId);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion
    }
}
