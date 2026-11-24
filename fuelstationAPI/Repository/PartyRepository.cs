using fuelstationAPI.Data;
using fuelstationAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;
namespace fuelstationAPI.Repository
{
    public class PartyRepository
    {
        private readonly DatabaseConnection _db;

        // Constructor
        public PartyRepository(DatabaseConnection db)
        {
            _db = db;
        }
          
        // Save Party Method
        public async Task AddPartyAsync(PartyModel model)
        {
            using (SqlConnection con = _db.GetConnection()) 
            {
                using (SqlCommand cmd = new SqlCommand("sp_AddParty", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
 
                    cmd.Parameters.Add("@PartyName", SqlDbType.VarChar, 100)
                        .Value = model.PartyName;

                    cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 15)
                        .Value = (object?)model.MobileNo ?? DBNull.Value;

                    cmd.Parameters.Add("@City", SqlDbType.VarChar, 50)
                        .Value = (object?)model.City ?? DBNull.Value;

                    cmd.Parameters.Add("@CreditLimit", SqlDbType.Decimal)
                        .Value = model.CreditLimit;

                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

