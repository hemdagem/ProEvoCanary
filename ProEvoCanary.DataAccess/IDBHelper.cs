using System.Data;

namespace ProEvoCanary.DataAccess
{
    public interface IDbHelper
    {
        int ExecuteScalar(string storedProcedure, object param = null);
        int ExecuteNonQuery(string storedProcedure, object param = null);
        IDataReader ExecuteReader(string commandText, object param = null);
    }
}