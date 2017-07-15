using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using ProEvoCanary.Domain.Helpers.Interfaces;

namespace ProEvoCanary.Domain.Helpers
{
    public class DbHelper : IDbHelper
    {
        private readonly string _connection;

        public DbHelper(IConfiguration connection)
        {
            _connection = connection.GetConfig();

        }
        public int ExecuteScalar(string storedProcedure, IDictionary<string, IConvertible> parameters = null)
        {
            using (SqlConnection db = new SqlConnection(_connection))
            {
                db.Open();
                return Convert.ToInt32(db.ExecuteScalar(storedProcedure, parameters, null, 30, CommandType.StoredProcedure));
            }
        }

        public int ExecuteNonQuery(string storedProcedure, IDictionary<string, IConvertible> parameters = null)
        {
            using (SqlConnection db = new SqlConnection(_connection))
            {
                db.Open();
                return db.Execute(storedProcedure, parameters, null, 30, CommandType.StoredProcedure);
            }
        }

        public IDataReader ExecuteReader(string storedProcedure, IDictionary<string, IConvertible> parameters = null)
        {
            SqlConnection db = new SqlConnection(_connection);
            db.Open();
            return db.ExecuteReader(storedProcedure, parameters, null, 30, CommandType.StoredProcedure);

        }

    }
}


