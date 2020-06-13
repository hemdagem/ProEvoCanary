using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Caching.Memory;

namespace ProEvoCanary.DataAccess
{
	public class InMemoryDbHelper : IDbHelper
	{
		private readonly string _connection;
		private MemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

		public InMemoryDbHelper(IDBConfiguration connection)
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

		public IDataReader ExecuteReader<T>(string commandText, object param = null)
		{
			
			throw new NotImplementedException();
		}
	}
}


