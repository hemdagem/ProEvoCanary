using System;
using System.Data;
using System.Data.SqlClient;
using ProEvoCanary.Helpers.Interfaces;

namespace ProEvoCanary.Helpers
{
    public class DBHelper : IDBHelper
    {
        private readonly IConfiguration _connectionString;
        private readonly IDbConnection _connection;
        private readonly IDbCommand _sqlCommand;
        private readonly int _commandCommandTimeout = 30;


        public DBHelper(IConfiguration configuration, IDbConnection connection, IDbCommand command, int commandTimeout)
        {
            _connectionString = configuration;
            _connection = connection;
            _sqlCommand = command;
            _commandCommandTimeout = commandTimeout;

        }

        public DBHelper() : this(new Configuration(), new SqlConnection(), new SqlCommand(), 30)
        {
            _connection.ConnectionString = _connectionString.GetConfig();
            _sqlCommand = new SqlCommand { CommandTimeout = _commandCommandTimeout, Connection = _connection as SqlConnection, CommandType = CommandType.StoredProcedure };

        }
        private void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed) _connection.Close();
        }
        public int ExecuteScalar(string storedProcedure)
        {

            var identity = 0;
            try
            {
                _sqlCommand.CommandText = storedProcedure;
                _connection.Open();
                identity = Convert.ToInt32(_sqlCommand.ExecuteScalar());

            }
            catch (Exception e)
            {
            }
            finally
            {
                CloseConnection();
            }
            return identity;
        }

        public int ExecuteNonQuery(string storedProcedure)
        {
            var iRowsAffected = 0;

            try
            {
                _connection.Open();
                _sqlCommand.CommandText = storedProcedure;
                iRowsAffected = _sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
            }
            finally
            {
                CloseConnection();
            }
            return iRowsAffected;
        }

        public IDataReader ExecuteReader(string storedProcedure)
        {
            try
            {
                _connection.Open();
                _sqlCommand.CommandText = storedProcedure;
                return _sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Database error: {0}", storedProcedure), e);

            }
        }

        public void AddParameter(string parameterName, object value)
        {
            _sqlCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void ClearParameters()
        {
            _sqlCommand.Parameters.Clear();
        }
    }
}


