using System.Data;
using fuelstationAPI.Data;
using fuelstationAPI.Interfaces;
using fuelstationAPI.Models;
using Microsoft.Data.SqlClient;

namespace fuelstationAPI.Repository
{
    public class PartyRepository : IPartyRepository
    {
        private readonly DatabaseConnection _db;

        public PartyRepository(DatabaseConnection db)
        {
            _db = db;
        }

        public async Task<int> AddPartyAsync(PartyModel model)
        {
            using (SqlConnection con = _db.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_AddParty", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                AddPartyParameters(cmd, model);

                await con.OpenAsync();
                object? result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task<List<PartyModel>> GetPartiesAsync()
        {
            var parties = new List<PartyModel>();

            using (SqlConnection con = _db.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_GetParties", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        parties.Add(MapParty(reader));
                    }
                }
            }

            return parties;
        }

        public async Task<PartyModel?> GetPartyByIdAsync(int partyId)
        {
            using (SqlConnection con = _db.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_GetPartyById", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PartyId", SqlDbType.Int).Value = partyId;

                await con.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return MapParty(reader);
                    }
                }
            }

            return null;
        }

        public async Task<bool> UpdatePartyAsync(PartyModel model)
        {
            using (SqlConnection con = _db.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_UpdateParty", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PartyId", SqlDbType.Int).Value = model.PartyId;
                AddPartyParameters(cmd, model);

                await con.OpenAsync();
                object? result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result) > 0;
            }
        }

        public async Task<bool> DeletePartyAsync(int partyId)
        {
            using (SqlConnection con = _db.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_DeleteParty", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PartyId", SqlDbType.Int).Value = partyId;

                await con.OpenAsync();
                object? result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result) > 0;
            }
        }

        private static void AddPartyParameters(SqlCommand cmd, PartyModel model)
        {
            cmd.Parameters.Add("@PartyName", SqlDbType.VarChar, 100).Value = model.PartyName;
            cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 15).Value = (object?)model.MobileNo ?? DBNull.Value;
            cmd.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = (object?)model.City ?? DBNull.Value;

            SqlParameter creditLimit = cmd.Parameters.Add("@CreditLimit", SqlDbType.Decimal);
            creditLimit.Precision = 18;
            creditLimit.Scale = 2;
            creditLimit.Value = model.CreditLimit;
        }

        private static PartyModel MapParty(SqlDataReader reader)
        {
            return new PartyModel
            {
                PartyId = reader.GetInt32(reader.GetOrdinal("PartyId")),
                PartyName = reader.IsDBNull(reader.GetOrdinal("PartyName"))
                    ? string.Empty
                    : reader.GetString(reader.GetOrdinal("PartyName")),
                MobileNo = reader.IsDBNull(reader.GetOrdinal("MobileNo"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("MobileNo")),
                City = reader.IsDBNull(reader.GetOrdinal("City"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("City")),
                CreditLimit = reader.IsDBNull(reader.GetOrdinal("CreditLimit"))
                    ? 0
                    : reader.GetDecimal(reader.GetOrdinal("CreditLimit"))
            };
        }
    }
}
