using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace ProEvoCanary.Domain.Helpers.Interfaces
{
    public interface IDbHelper
    {
        int ExecuteScalar(string storedProcedure, object param = null);
        int ExecuteNonQuery(string storedProcedure, IDictionary<string, IConvertible> parameters = null);
        IDataReader ExecuteReader(string commandText, object param = null);
        SqlMapper.GridReader ExecuteReaderMultiple(string storedProcedure, object param = null);
    }
}