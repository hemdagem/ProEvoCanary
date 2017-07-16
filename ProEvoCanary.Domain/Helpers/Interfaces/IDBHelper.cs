using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace ProEvoCanary.Domain.Helpers.Interfaces
{
    public interface IDbHelper
    {
        int ExecuteScalar(string commandText, IDictionary<string, IConvertible> parameters = null);
        int ExecuteNonQuery(string storedProcedure, IDictionary<string, IConvertible> parameters = null);
        IDataReader ExecuteReader(string commandText, IDictionary<string, IConvertible> parameters = null);
        SqlMapper.GridReader ExecuteReaderMultiple(string storedProcedure, object param = null);
    }
}