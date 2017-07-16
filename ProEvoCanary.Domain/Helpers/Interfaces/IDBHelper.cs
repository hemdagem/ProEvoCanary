using System.Data;

namespace ProEvoCanary.Domain.Helpers.Interfaces
{
    public interface IDbHelper
    {
        int ExecuteScalar(string storedProcedure, object param = null);
        int ExecuteNonQuery(string storedProcedure, object param = null);
        IDataReader ExecuteReader(string commandText, object param = null);
    }
}