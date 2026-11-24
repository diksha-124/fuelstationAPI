using System.Data;
using fuelstationAPI.Data;
using fuelstationAPI.Interfaces;
using fuelstationAPI.Models;
using Microsoft.Data.SqlClient;

namespace fuelstationAPI.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DatabaseConnection _db;

        public LoginRepository(DatabaseConnection db)
        {
            _db = db;
        }

        public async Task<UserModel?> GetByUserNameAsync(string userName)
        {
            using (SqlConnection con = _db.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_GetUserByUserName", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = userName;

                await con.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (!await reader.ReadAsync())
                    {
                        return null;
                    }

                    return new UserModel
                    {
                        UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                        UserName = reader.GetString(reader.GetOrdinal("UserName")),
                        PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"))
                    };
                }
            }
        }
    }
}
