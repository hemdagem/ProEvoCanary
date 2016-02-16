using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ProEvoCanary.Domain.Helpers.Interfaces;

namespace ProEvoCanary.Domain.Helpers
{
    public class DbHelper : IDbHelper
    {
        private readonly IConfiguration _connectionString;
        private readonly IDbConnection _connection;
        private readonly IDbCommand _sqlCommand;
        private readonly int _commandCommandTimeout;

        public DbHelper(IConfiguration configuration, IDbConnection connection, IDbCommand command, int commandTimeout)
        {
            _connectionString = configuration;
            _connection = connection;
            _sqlCommand = command;
            _commandCommandTimeout = commandTimeout;
        }

        public DbHelper() : this(new Configuration(), new SqlConnection(), new SqlCommand(), 30)
        {
            _connection.ConnectionString = _connectionString.GetConfig();
            _sqlCommand = new SqlCommand { CommandTimeout = _commandCommandTimeout, Connection = _connection as SqlConnection, CommandType = CommandType.StoredProcedure };
        }

        private void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed) _connection.Close();
        }

        public int ExecuteScalar(string storedProcedure, IDictionary<string, IConvertible> parameters =null)
        {
            int identity;

            try
            {
                _sqlCommand.CommandText = storedProcedure;
                _connection.Open();
                AddParameters(parameters);
                identity = Convert.ToInt32(_sqlCommand.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Database error: {0}", storedProcedure), e);
            }
            finally
            {
                CloseConnection();
            }
            return identity;
        }

        public int ExecuteNonQuery(string storedProcedure, IDictionary<string, IConvertible> parameters=null)
        {
            int iRowsAffected;

            try
            {
                _connection.Open();
                _sqlCommand.CommandText = storedProcedure;
                AddParameters(parameters);
                iRowsAffected = _sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Database error: {0}", storedProcedure), e);
            }
            finally
            {
                CloseConnection();
            }
            return iRowsAffected;
        }

        public IDataReader ExecuteReader(string storedProcedure, IDictionary<string, IConvertible> parameters=null)
        {
            try
            {
                _connection.Open();
                _sqlCommand.CommandText = storedProcedure;
                AddParameters(parameters);
                return _sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Database error: {0}", storedProcedure), e);
            }
        }

        private void AddParameters(IDictionary<string, IConvertible> parameters)
        {
            _sqlCommand.Parameters.Clear();

            if (parameters == null || parameters.Count <= 0) return;

            foreach (var convertible in parameters)
            {
                _sqlCommand.Parameters.Add(new SqlParameter(convertible.Key, convertible.Value));
            }
        }
    }
}


