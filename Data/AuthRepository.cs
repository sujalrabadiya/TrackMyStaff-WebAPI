using Microsoft.Data.SqlClient;
using System.Data;
using TrackMyStaffWebApplication.Models;

namespace TrackMyStaffWebApplication.Data
{
    public class AuthRepository
    {
        private readonly string _configuration;

        public AuthRepository(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("ConnectionString");
        }

        #region Register Company
        public bool Insert(RegisterationModel registerationModel)
        {
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Business_Insert";
                command.Parameters.AddWithValue("@CName", registerationModel.CName);
                command.Parameters.AddWithValue("@Email", registerationModel.Email);
                command.Parameters.AddWithValue("@Phone", registerationModel.Phone);
                command.Parameters.AddWithValue("@UName", registerationModel.UName);
                command.Parameters.AddWithValue("@Password", registerationModel.Password);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Login
        public ProfileModel Login(string phone, string password)
        {
            ProfileModel user = null;
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("PR_User_Login", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Password", password);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new ProfileModel
                    {
                        id = Convert.ToInt32(reader["id"]),
                        name = reader["name"].ToString(),
                        email = reader["email"].ToString(),
                        role = reader["role"].ToString(),
                        img_url = reader["img_url"].ToString(),
                        company_id = Convert.ToInt32(reader["company_id"]),
                        company_name = reader["company_name"].ToString()
                    };
                }
            }
            return user;
        }
        #endregion
    }
}
