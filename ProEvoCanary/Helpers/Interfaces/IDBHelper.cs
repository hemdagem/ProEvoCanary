using System;
using System.Collections.Generic;
using System.Data;

namespace ProEvoCanary.Helpers.Interfaces
{
    public interface IDBHelper
    {
        int ExecuteScalar(string commandText, IDictionary<string, IConvertible> parameters = null);
        int ExecuteNonQuery(string storedProcedure, IDictionary<string, IConvertible> parameters = null);
        IDataReader ExecuteReader(string commandText, IDictionary<string, IConvertible> parameters = null);
    }
}