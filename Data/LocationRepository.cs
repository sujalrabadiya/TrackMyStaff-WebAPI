using Microsoft.Data.SqlClient;
using System.Data;
using TrackMyStaffWebApplication.Models;

namespace TrackMyStaffWebApplication.Data
{
    public class LocationRepository
    {
        private readonly string _configuration;

        public LocationRepository(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("ConnectionString");
        }

        #region Add Location

        public string GetUserNameById(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT name FROM Users WHERE id = @UserId", connection);
                cmd.Parameters.AddWithValue("@UserId", userId);

                var result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Unknown User";
            }
        }

        public bool AddLocation(LocationModel locationModel)
        {
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("PR_Location_AddUpdate", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserId", locationModel.UserId);
                cmd.Parameters.AddWithValue("@Latitude", locationModel.Latitude);
                cmd.Parameters.AddWithValue("@Longitude", locationModel.Longitude);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        //#region Get Live Locations By Staff Id
        //public LocationModel GetLocation(int userId)
        //{
        //    LocationModel location = null;
        //    using (SqlConnection connection = new SqlConnection(_configuration))
        //    {
        //        connection.Open();
        //        SqlCommand cmd = new SqlCommand("PR_Location_GetLiveByStaff", connection)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };
        //        cmd.Parameters.AddWithValue("@UserId", userId);

        //        SqlDataReader reader = cmd.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            location = new LocationModel
        //            {
        //                UserName = reader["name"].ToString(),
        //                Latitude = Convert.ToDecimal(reader["latitude"]),
        //                Longitude = Convert.ToDecimal(reader["longitude"]),
        //                Timestamp = Convert.ToDateTime(reader["timestamp"])
        //            };
        //        }
        //    }
        //    return location;
        //}
        //#endregion

        #region Get Live Locations By Company
        public IEnumerable<LocationModel> GetLiveLocationsByCompany(int companyId)
        {
            var locations = new List<LocationModel>();
            using (SqlConnection connection = new SqlConnection(_configuration))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("PR_Location_GetLiveByCompany", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CompanyId", companyId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    locations.Add(new LocationModel
                    {
                        UserName = reader["name"].ToString(),
                        Latitude = Convert.ToDecimal(reader["latitude"]),
                        Longitude = Convert.ToDecimal(reader["longitude"]),
                        Timestamp = Convert.ToDateTime(reader["timestamp"])
                    });
                }
            }
            return locations;
        }
        #endregion

        //#region Get Location History
        //public IEnumerable<LocationModel> GetLocationHistory(int userId, DateTime startDate, DateTime endDate)
        //{
        //    var history = new List<LocationModel>();
        //    using (SqlConnection connection = new SqlConnection(_configuration))
        //    {
        //        connection.Open();
        //        SqlCommand cmd = new SqlCommand("PR_Location_GetHistoryByStaff", connection)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };
        //        cmd.Parameters.AddWithValue("@UserId", userId);
        //        cmd.Parameters.AddWithValue("@StartDate", startDate);
        //        cmd.Parameters.AddWithValue("@EndDate", endDate);

        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            history.Add(new LocationModel
        //            {
        //                Latitude = Convert.ToDecimal(reader["latitude"]),
        //                Longitude = Convert.ToDecimal(reader["longitude"]),
        //                Timestamp = Convert.ToDateTime(reader["timestamp"])
        //            });
        //        }
        //    }
        //    return history;
        //}
        //#endregion
    }
}
