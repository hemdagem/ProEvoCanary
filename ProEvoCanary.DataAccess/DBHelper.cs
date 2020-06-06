using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using ProEvoCanary.Domain.Helpers.Interfaces;

namespace ProEvoCanary.DataAccess.Helpers
{
	public class DbHelper : IDbHelper
	{
		private readonly string _connection;

		public DbHelper(IDBConfiguration connection)
		{
			_connection = connection.GetConfig();

		}
		public int ExecuteScalar(string storedProcedure, object param = null)
		{
			try
			{
				using SqlConnection db = new SqlConnection(_connection);
				db.Open();
				return Convert.ToInt32(db.ExecuteScalar(storedProcedure, param, null, 30,
					CommandType.StoredProcedure));
			}
			catch (Exception e)
			{
				Console.Error.WriteLine(e);

			}

			return -1;
		}

		public int ExecuteNonQuery(string storedProcedure, object param = null)
		{
			try
			{
				using SqlConnection db = new SqlConnection(_connection);
				db.Open();
				return db.Execute(storedProcedure, param, null, 30, CommandType.StoredProcedure);
			}
			catch (Exception e)
			{
				Console.Error.WriteLine(e);

			}

			return -1;
		}

		public IDataReader ExecuteReader(string storedProcedure, object param = null)
		{
			try
			{
				SqlConnection db = new SqlConnection(_connection);
				db.Open();
				return db.ExecuteReader(storedProcedure, param, null, 30, CommandType.StoredProcedure);
			}
			catch (Exception e)
			{
				Console.Error.WriteLine(e);

			}

			return new DataTableReader(new DataTable());
		}
	}
}


