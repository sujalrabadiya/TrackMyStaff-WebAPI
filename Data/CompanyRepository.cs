using Microsoft.Data.SqlClient;
using System.Data;
using TrackMyStaffWebApplication.Models;

namespace TrackMyStaffWebApplication.Data
{
    public class CompanyRepository
    {
        #region Configuration
        private readonly string _configuration;
        public CompanyRepository(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region Select All
        public IEnumerable<CompanyModel> GetAll()
        {
            var companyList = new List<CompanyModel>();
            SqlConnection connection = new SqlConnection(_configuration);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Business_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                companyList.Add(new CompanyModel()
                {
                    id = Convert.ToInt32(reader["id"]),
                    name = reader["name"].ToString(),
                    email = reader["email"].ToString(),
                    phone = reader["phone"].ToString(),
                    created_at = Convert.ToDateTime(reader["created_at"]),
                    updated_at = Convert.ToDateTime(reader["updated_at"])
                });
            }
            return companyList;
        }
        #endregion

        #region Get By ID
        public CompanyModel GetById(int id)
        {
            CompanyModel company = null;
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Business_SelectByPK";
                command.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    company = new CompanyModel()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        name = reader["name"].ToString(),
                        email = reader["email"].ToString(),
                        phone = reader["phone"].ToString(),
                        created_at = Convert.ToDateTime(reader["created_at"]),
                        updated_at = Convert.ToDateTime(reader["updated_at"])
                    };
                }
            }
            return company;
        }
        #endregion

        #region Update
        public bool Update(CompanyModel company)
        {
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Business_UpdateByPK";
                command.Parameters.AddWithValue("@ID", company.id);
                command.Parameters.AddWithValue("@Name", company.name);
                command.Parameters.AddWithValue("@Email", company.email);
                command.Parameters.AddWithValue("@Phone", company.phone);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Delete
        public bool Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Business_DeleteByPK";
                command.Parameters.AddWithValue("@ID", id);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion
    }
}
